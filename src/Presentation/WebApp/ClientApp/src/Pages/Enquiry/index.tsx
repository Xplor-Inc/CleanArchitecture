import { Component } from "react"
import { FaEdit, FaEye } from "react-icons/fa";
import { toast } from "react-toastify";
import { API_END_POINTS } from "../../Components/Core/Constants/EndPoints";
import { IEnquiryDto } from "../../Components/Core/Dto/Enquiry";
import { IResult } from "../../Components/Core/Dto/IResultObject";
import { IPagingResult } from "../../Components/Core/Dto/Paging";
import SearchParams from "../../Components/Core/Dto/SearchParams";
import { Service } from "../../Components/Service";
import { Utility } from "../../Components/Service/Utility";
import { Loader } from "../Components/Loader";
import PageTitle from "../Components/Navigation/PageTitle";
import { Paging } from "../Components/Paging";
import PopupWindow from "../Components/PopupWindow";
import { ToastError } from "../Components/ToastError";

interface IEnquiryState {
    isUpdating: boolean
    paging: IPagingResult
    search: SearchParams
    enquiries: IEnquiryDto[]
    isLoading: boolean
    resolution: string
    id?: string,
    viewResolution?: string
}

export default class Users extends Component<{}, IEnquiryState>{

    constructor(props: {}) {
        super(props)

        this.state = {
            isUpdating: false,
            enquiries: [],
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
            resolution: '',
            id: ''
        }
    }

    async componentDidMount() {
        this.getEnquiries(0, this.state.search);
    }

    searchEnquiries = async () => {
        if (Utility.Validate.String(this.state.search.searchText))
            this.getEnquiries(0,this.state.search)
    }

    getEnquiries = async (skip: number, s: SearchParams) => {
        var q = `?skip=${skip}&take=${s.take}&sortBy=${s.sortBy}&sortOrder=${s.sortOrder}`;
        if (Utility.Validate.String(s.searchText))
            q += `&searchText=${s.searchText}`;

        var response = await Service.Get<IResult<IEnquiryDto[]>>(`${API_END_POINTS.ENQUIRY}${q}`);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            this.setState({ isLoading: false });
            return;
        }

        if (response.resultObject) {
            var paging = Service.ApplyPaging(this.state.search.skip, response.rowCount, this.state.search.take);
            this.setState({
                isLoading: false, enquiries: response.resultObject,
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
        this.getEnquiries(skip, this.state.search);
    }

    prevPage = async () => {
        var skip = this.state.search.skip - this.state.search.take;
        this.setState({ isLoading: true, search: { ...this.state.search, skip: skip } });
        this.getEnquiries(skip, this.state.search);
    }

    postResolution = async (e: React.MouseEvent<HTMLAnchorElement | HTMLButtonElement>) => {
        e.preventDefault();
        var formData = {
            id: this.state.id,
            resolution: this.state.resolution
        };

        this.setState({ isLoading: true })
        var updateResponse = await Service.Put<IResult<boolean>>(`${API_END_POINTS.ENQUIRY}/${this.state.id}`, formData);

        this.setState({ isLoading: false })
        if (updateResponse.hasErrors) {
            toast.error(<ToastError errors={updateResponse.errors} />);
            return;
        }
        this.setState({ isUpdating: false, id: '' })
        this.getEnquiries(this.state.search.skip, this.state.search);
        toast.success(`Resolutio updated successfully`);
    }

    viewResolution = async (resolution: string) => {
        toast.success(resolution, { position: toast.POSITION.TOP_CENTER, autoClose: false })
    }

    render() {
        const { isLoading, enquiries, resolution, isUpdating, viewResolution } = this.state
        return (
            <>
                <div className="card m-3">
                    {isLoading ? <Loader /> : null}
                    <PageTitle title='Review Enquiries' />
                    <h3 className="card-header">Review Enquiries</h3>
                    <div className="card-body">
                        <form method="post">
                            <div className="row">
                                <div className="col-md-6">
                                    <span>Resolution</span>
                                    <input type="text" placeholder="Search" className="form-control"
                                        value={this.state.search.searchText}
                                        onChange={(e) => { this.setState({ search: { ...this.state.search, searchText: e.target.value } }) }} />
                                </div>
                                <div className="col-md-6 button-pt">
                                    <button type="button" className="btn btn-outline-info" onClick={this.searchEnquiries}> Search</button>
                                    <button type="button" className="btn btn-outline-warning ms-3" onClick={()=>{this.setState({search:new SearchParams()}); this.getEnquiries(0, new SearchParams());}}> Reset</button>
                               </div>
                            </div>
                        </form>
                        <hr />
                        <div className="row">
                            {
                                enquiries.length > 0 ?
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
                                        <th>Name</th>
                                        <th>Email</th>
                                        <th>Subject</th>
                                        <th>Message</th>
                                        <th>Created On</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {
                                        enquiries.map((enquiry, index) => {
                                            return <tr key={enquiry.id}>
                                                <td>{index + 1}</td>
                                                <td>{enquiry.name}</td>
                                                <td>{enquiry.email}</td>
                                                <td>{enquiry.subject}</td>
                                                <td>{enquiry.message}</td>
                                                <td>{Utility.Format.DateTime_DD_MMM_YY_HH_MM_SS(enquiry.createdOn)}</td>
                                                <td>
                                                    {
                                                        enquiry.isResolved ?
                                                            <a href="#edit" title='Edit' onClick={() => this.setState({viewResolution: enquiry.resolution})}>
                                                                <FaEye size={'1.4rem'} color='#755139FF' />
                                                            </a> :
                                                            <a href="#edit" title='Edit' onClick={() => this.setState({ isUpdating: true, id: enquiry.id, })}>
                                                                <FaEdit size={'1.4rem'} color='#755139FF' />
                                                            </a>
                                                    }
                                                </td>
                                            </tr>
                                        })
                                    }
                                    {
                                        enquiries.length === 0 ?
                                        <tr>
                                            <td colSpan={7}>There is no enquiry data</td>
                                        </tr> : null
                                    }
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                {
                    isUpdating ?
                        <PopupWindow heading="Set Resolution" onClose={() => { this.setState({ isUpdating: false, id: '' }) }}>
                            <form method="post">
                                <div className="row">
                                    <div className="col-md-6">
                                        <span>Resolution</span>
                                        <input type="text" placeholder="Enquiry resolution" className="form-control"
                                            value={resolution}
                                            onChange={(e) => { this.setState({ resolution: e.target.value }) }} />
                                    </div>
                                    <div className="col-md-6" style={{ paddingTop: '20px' }}>
                                        <button type="button" className="btn btn-outline-info" onClick={(e) => this.postResolution(e)}> Update</button>
                                    </div>
                                </div>
                            </form>
                        </PopupWindow>
                        : null
                }
                {
                    viewResolution ?
                        <PopupWindow heading="Resolution" onClose={() => { this.setState({ viewResolution: undefined }) }}>
                            <form method="post">
                                <div className="row">
                                    <div className="col-md-12">
                                        <span>Resolution</span>
                                        <input type="text" placeholder="Enquiry resolution" className="form-control"
                                            value={viewResolution} disabled />
                                    </div>
                                </div>
                            </form>
                        </PopupWindow>
                        : null
                }
            </>
        )
    }
}