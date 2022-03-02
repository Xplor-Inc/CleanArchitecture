import { FaKey } from 'react-icons/fa'
import React, { Component } from 'react'
import { Service } from '../../Components/Service'
import { API_END_POINTS } from '../../Components/Core/Constants/EndPoints'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import PageTitle from '../Components/Navigation/PageTitle';
import { ToastError } from '../Components/ToastError';
import { Loader } from '../Components/Loader';
import { IResult } from '../../Components/Core/Dto/IResultObject';
import { IUserDto } from '../../Components/Core/Dto/Users';

interface IChangePasswordState {
    buttonText: string
    confirmPassword: string,
    oldPassword: string,
    newPassword: string,
    isLoading: boolean,
    isValid: boolean
}
class ChangePassword extends Component<any, IChangePasswordState> {
    constructor(props: any) {
        super(props)
        toast.dismiss();

        this.state = {
            buttonText: 'Change Password',
            newPassword: '',
            oldPassword: '',
            confirmPassword: '',
            isValid: false,
            isLoading: false,
        }
    }

    chnagePassword = async (e: React.MouseEvent<HTMLInputElement>) => {
        e.preventDefault();
        var errors = [];
        const { newPassword, oldPassword, confirmPassword } = this.state;
        if (newPassword.length === 0) {
            errors.push('Please enter new password');
        }
        if (oldPassword.length === 0) {
            errors.push('Please enter old password');
        }
        if (confirmPassword.length === 0) {
            errors.push('Please enter confirm password');
        }
        if (newPassword !== confirmPassword) {
            errors.push('New Password and Confirm Password not match');
        }
        if (errors.length > 0) {
            toast.error(<ToastError errors={errors} />);
            return;
        }
        this.setState({ isLoading: true, buttonText: 'Changing...' });
        var body = {
            newPassword: newPassword,
            oldPassword: oldPassword
        }
        var response = await Service.Post<IResult<IUserDto>>(API_END_POINTS.CHANGE_PASSWORD, body);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            this.setState({ isLoading: false, buttonText: 'Change Password' });
            return;
        }
        this.setState({ isLoading: false, newPassword: '', oldPassword: '', confirmPassword: '', buttonText: 'Password Changed' });
        toast.success(<ToastError errors={["Password reset successfully"]} />)

    }
    render() {
        const { isLoading, newPassword, confirmPassword, oldPassword, buttonText } = this.state
        return (
            <div className="row">
                {isLoading ? <Loader /> : null}
                <PageTitle title='Change Password' />
                <div className="col-md-3">
                </div>
                <div className="col-md-6">
                    <div className="card mb-3 mt-4">
                        <div id="per86" className="collapse show">
                            <div className="card-body">
                                <form method="post" action="/account/login">
                                    <div className="row">
                                        <div className="col-md-12">
                                            <h2>Change Password</h2>
                                            <hr className="mt-0 pt-0" />
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-md-12">
                                            <label>Old Password</label>
                                            <div className="input-group">
                                                <div className="input-group-prepend">
                                                    <span className="input-group-text">
                                                        <FaKey style={{ height: '1.5rem' }} />
                                                    </span>
                                                </div>
                                                <input type="password" className="form-control" placeholder="Enter Old Password"
                                                    value={oldPassword}
                                                    onChange={(e) => this.setState({ oldPassword: e.target.value })} />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-md-12">
                                            <label>New Password</label>
                                            <div className="input-group">
                                                <div className="input-group-prepend">
                                                    <span className="input-group-text">
                                                        <FaKey style={{ height: '1.5rem' }} />
                                                    </span>
                                                </div>
                                                <input type="password" className="form-control" placeholder="Enter New Password"
                                                    value={newPassword}
                                                    onChange={(e) => this.setState({ newPassword: e.target.value })} />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-md-12">
                                            <label>Confirm Password</label>
                                            <div className="input-group">
                                                <div className="input-group-prepend">
                                                    <span className="input-group-text">
                                                        <FaKey style={{ height: '1.5rem' }} />
                                                    </span>
                                                </div>
                                                <input type="password" className="form-control" placeholder="Enter Confirm Password"
                                                    value={confirmPassword}
                                                    onChange={(e) => this.setState({ confirmPassword: e.target.value })} />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-md-6 col-6 text-right pt-2">
                                            <input type="submit" className="btn btn-outline-info" disabled={buttonText !== 'Change Password'}
                                                value={buttonText}
                                                onClick={this.chnagePassword} />
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}
export default ChangePassword;
