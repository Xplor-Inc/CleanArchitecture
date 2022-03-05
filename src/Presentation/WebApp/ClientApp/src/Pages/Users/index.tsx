import { Component } from "react"
import { FaEdit, FaToggleOff, FaToggleOn, FaTrash } from "react-icons/fa";
import { toast } from "react-toastify";
import { API_END_POINTS } from "../../Components/Core/Constants/EndPoints";
import { IResult } from "../../Components/Core/Dto/IResultObject";
import { IPagingResult } from "../../Components/Core/Dto/Paging";
import { IUserDto } from "../../Components/Core/Dto/Users";
import { Service } from "../../Components/Service";
import { Utility } from "../../Components/Service/Utility";
import { Loader } from "../Components/Loader";
import PageTitle from "../Components/Navigation/PageTitle";
import { Paging } from "../Components/Paging";
import { ToastError } from "../Components/ToastError";

class SearchParams {
    sortOrder: string = "ASC"
    sortBy: string = "FirstName"
    skip: number = 0
    take: number = 2
    searchText: string = ''
    userRole?: number
    includeDeleted: boolean = false
}
export default class Users extends Component<{}, { isUpdating: boolean, paging: IPagingResult, search: SearchParams, users: IUserDto[], isLoading: boolean, user: IUserDto }>{

    constructor(props: {}) {
        super(props)

        this.state = {
            isUpdating: false,
            users: [],
            isLoading: true,
            paging: {
                nextDisabled: true,
                prevDisabled: true,
                text: {
                    from: 0,
                    to: 0,
                    count: 0
                },
                prevPage: this.prevPage,
                nextPage: this.nextPage
            },
            search: new SearchParams(),
            user: {
                emailAddress: '',
                firstName: '',
                isActive: false,
                lastName: '',
                id: '',
                imagePath:''
            }
        }
    }

    async componentDidMount() {

        this.getUsers(0);
    }

    getUsers = async (skip: number) => {
        var s = this.state.search;
        var q = `?skip=${skip}&take=${s.take}&sortBy=${s.sortBy}&sortOrder=${s.sortOrder}`;
        if (Utility.Validate.String(s.searchText))
            q += `&searchText=${s.searchText}`;
        if (s.includeDeleted)
            q += `&includeDeleted=${s.includeDeleted}`;

        var response = await Service.Get<IResult<IUserDto[]>>(`${API_END_POINTS.USERS}${q}`);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            this.setState({ isLoading: false });
            return;
        }
        if (response.resultObject) {
            var paging = Service.ApplyPaging(this.state.search.skip, response.rowCount, this.state.search.take);
            this.setState({
                isLoading: false, users: response.resultObject,
                paging: {
                    ...this.state.paging,
                    text: paging.text, nextDisabled: paging.nextDisabled,
                    prevDisabled: paging.prevDisabled
                }
            });

        }
        else {
            toast.info('Currently there is no data')
            this.setState({ isLoading: false });
        }
    }

    nextPage = async () => {
        var skip = this.state.search.skip + this.state.search.take;
        this.setState({ isLoading: true, search: { ...this.state.search, skip: skip } });
        this.getUsers(skip);
    }

    prevPage = async () => {
        var skip = this.state.search.skip - this.state.search.take;
        this.setState({ isLoading: true, search: { ...this.state.search, skip: skip } });
        this.getUsers(skip);
    }

    createUser = async (e: React.MouseEvent<HTMLButtonElement>) => {
        const { firstName, lastName, emailAddress } = this.state.user;
        e.preventDefault();
        var errors = [];
        if (!Utility.Validate.String(firstName)) {
            errors.push('Please enter first name');
        }
        if (!Utility.Validate.String(lastName)) {
            errors.push('Please enter last name');
        }
        if (!Utility.Validate.String(emailAddress)) {
            errors.push('Please enter email address');
        }
        if (errors.length > 0) {
            toast.error(<ToastError errors={errors} />);
            return;
        }
        this.setState({ isLoading: true });
        var formData = {
            firstName: firstName,
            lastName: lastName,
            emailAddress:emailAddress
        }
        var response = await Service.Post<IResult<IUserDto>>(API_END_POINTS.USERS, formData);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            this.setState({
                isLoading: false
            })
            return;
        }
        toast.success(`User account created succssfully`);
        this.setState({
            isLoading: false,
            user: {
                ...this.state.user,
                firstName: '',
                lastLoginDate: undefined,
                lastName: '',
                emailAddress: ''
            }
        })
        this.getUsers(0);
    }

    deleteUser = async (e: React.MouseEvent<HTMLAnchorElement>, user: IUserDto) => {
        e.preventDefault();
        if (window.confirm(`Are you sure to delete ${user.firstName} ?`)) {


            this.setState({ isLoading: true })
            var updateResponse = await Service.Delete<IResult<boolean>>(`${API_END_POINTS.USERS}/${user.id}`, {});

            this.setState({ isLoading: false })
            if (updateResponse.hasErrors) {
                toast.error(<ToastError errors={updateResponse.errors} />);
                return;
            }
            this.getUsers(this.state.search.skip);
            toast.success(`User ${user.firstName} updated successfully`);
        }
    }

    editUser = async (e: React.MouseEvent<HTMLAnchorElement>, user: IUserDto) => {
        e.preventDefault();
        this.setState({ isUpdating: true, user: user });
    }

    updateUser = async (e: React.MouseEvent<HTMLAnchorElement | HTMLButtonElement>, user: IUserDto) => {
        e.preventDefault();
        if (this.state.isUpdating || window.confirm(`Are you sure to  ${user.isActive ? "Block" : "unblock"} ${user.firstName} ?`)) {

            var formData = {
                id: user.id,
                isActive: (!user.isActive) ?? false,
                firstName: user.firstName,
                lastName: user.lastName,
                emailAddress: user.emailAddress
            };
            if (this.state.isUpdating)
                formData.isActive = user.isActive ?? false;
            this.setState({ isLoading: true })
            var updateResponse = await Service.Put<IResult<boolean>>(`${API_END_POINTS.USERS}/${user.id}`, formData);
            if (this.state.isUpdating) {
                this.setState({
                    isUpdating: false,
                    isLoading: false,
                    user: {
                        ...this.state.user,
                        firstName: '',
                        lastLoginDate: undefined,
                        lastName: '',
                        emailAddress: ''
                    }
                })
            }
            else
                this.setState({ isLoading: false })
            if (updateResponse.hasErrors) {
                toast.error(<ToastError errors={updateResponse.errors} />);
                return;
            }
            this.getUsers(this.state.search.skip);
            toast.success(`User ${user.firstName} updated successfully`);
        }
    }

    render() {
        const { isLoading, users, user, isUpdating } = this.state
        return (
            <div className="card m-3">
                {isLoading ? <Loader /> : null}
                <PageTitle title='Manage Users' />
                <h3 className="card-header">Create &amp; Manage User Accounts</h3>
                <div className="card-body">
                    <form method="post">
                        <div className="row">
                            <div className="col-md-3">
                                <span>First Name</span>

                                <input type="text" placeholder="First Name" className="form-control"
                                    value={user.firstName}
                                    onChange={(e) => { this.setState({ user: { ...this.state.user, firstName: e.target.value } }) }} />
                            </div>
                            <div className="col-md-3">
                                <span>Last Name</span>
                                <input type="text" placeholder="Last Name" className="form-control"
                                    value={user.lastName}
                                    onChange={(e) => { this.setState({ user: { ...this.state.user, lastName: e.target.value } }) }} />
                            </div>
                            <div className="col-md-3">
                                <span>Email Address</span>
                                <input type="text" placeholder="Email Address" className="form-control"
                                    disabled={isUpdating}
                                    value={user.emailAddress}
                                    onChange={(e) => { this.setState({ user: { ...this.state.user, emailAddress: e.target.value } }) }} />
                            </div>
                            <div className="col-md-3 button-pt">
                                <button type="button" className="btn btn-outline-info" onClick={(e) => this.createUser(e)}> {isUpdating ? "Update" : "Add"} User</button>
                                {
                                    isUpdating ?
                                    <button type="button" className="btn btn-outline-danger ms-2"
                                        onClick={() => this.setState({
                                            isUpdating: false,
                                            user: {
                                                ...this.state.user, firstName: '',
                                                lastLoginDate: undefined, lastName: '', emailAddress: ''
                                            }
                                        })}> Reset</button> : null}
                            </div>
                        </div>
                    </form>
                    <hr />
                    <div className="row">
                        {
                            users.length > 0 ?
                                <>
                                    <div className="text-end col-12 p-0">
                                        <Paging {...this.state.paging} />
                                    </div>
                                </>
                                : ''
                        }
                    </div>
                    <div className="table-responsive">
                        <table className="table" id="datatable">
                            <thead>
                                <tr>
                                    <th className="text-center">SR #</th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Email Address</th>
                                    <th>Last Login</th>
                                    <th>Created On</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    users.map((user, index) => {
                                        return <tr key={user.id}>
                                            <td>{index + 1}</td>
                                            <td>{user.firstName}</td>
                                            <td>{user.lastName}</td>
                                            <td>{user.emailAddress}</td>
                                            <td>{Utility.Format.DateTime_DD_MMM_YY_HH_MM_SS(user.createdOn)}</td>
                                            <td>{Utility.Format.DateTime_DD_MMM_YY_HH_MM_SS(user.lastLoginDate)}</td>
                                            <td>
                                                <a href='/'
                                                    onClick={(e) => { this.updateUser(e, user) }}
                                                    title={user.isActive ? "Block User" : "Unblock User"}>
                                                    {
                                                        user.isActive ?
                                                            <FaToggleOn className='text-success' size='2rem' /> :
                                                            <FaToggleOff className='text-danger' size='2rem' />
                                                    }
                                                </a>
                                                &nbsp;
                                                <a href='#delete' title='Delete User' onClick={(e) => this.deleteUser(e, user)}>
                                                    <FaTrash size={'1.4rem'} color='red' />
                                                </a>
                                                &nbsp;
                                                <a href="#edit" title='Edit' onClick={(e) => this.editUser(e, user)}>
                                                    <FaEdit size={'1.4rem'} color='#755139FF' />
                                                </a>
                                            </td>
                                        </tr>
                                    })
                                }
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        )
    }
}