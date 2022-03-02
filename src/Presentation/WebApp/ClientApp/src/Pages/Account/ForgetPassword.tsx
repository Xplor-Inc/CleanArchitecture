import { FaEnvelope } from 'react-icons/fa'
import React, { Component } from 'react'
import { Service } from '../../Components/Service'
import { API_END_POINTS } from '../../Components/Core/Constants/EndPoints'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import PageTitle from '../Components/Navigation/PageTitle';
import { ToastError } from '../Components/ToastError';
import { Loader } from '../Components/Loader';
import { NavLink } from 'react-router-dom';
import { ROUTE_END_POINTS } from '../../Components/Core/Constants/RouteEndPoints';
import { IResult } from '../../Components/Core/Dto/IResultObject';

class ForgetPassword extends Component<any, { email: string, buttonText: string, isLoading: boolean, message?: string }> {
    constructor(props: any) {
        super(props)
        this.state = {
            email: '',
            buttonText: 'Send Email',
            isLoading: false,
        }
        toast.dismiss();
    }

    processLogin = async (e: React.MouseEvent<HTMLInputElement>) => {
        e.preventDefault();
        var errors = [];
        if (this.state.email.length === 0) {
            errors.push('Please enter email address');
        }
        if (errors.length > 0) {
            toast.error(<ToastError errors={errors} />);
            return;
        }
        this.setState({ isLoading: true, buttonText: 'Sending..' });
        var body = {
            emailAddress: this.state.email
        }
        var response = await Service.Post<IResult<boolean>>(API_END_POINTS.FORGET_PASSWORD, body);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            this.setState({ isLoading: false, buttonText: 'Send Email' });
            return;
        }

        this.setState({ email:'', isLoading: false, buttonText: 'Email sent', message: 'Password recovery email sent to your registered email.' });
    }
    render() {
        const { isLoading, buttonText, email, message } = this.state
        return (
            <div className="row">
                <PageTitle title='Forget Password ' />
                <div className="col-md-3">
                </div>
                <div className="col-md-6">
                    <div className="card mb-3 mt-4">
                        <div id="per86" className="collapse show">
                            <div className="card-body">
                                <form method="post" action="/account/login">
                                    <div className="row">
                                        <div className="col-md-12">
                                            <h2>Forget Password</h2>
                                            <hr className="mt-0 pt-0" />
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-md-12">
                                            <label>Email Address</label>
                                            <div className="input-group">
                                                <div className="input-group-prepend">
                                                    <span className="input-group-text">
                                                        <FaEnvelope style={{ height: '1.5rem' }} />
                                                    </span>
                                                </div>
                                                <input type="email" className="form-control" placeholder="Enter Email Address"
                                                    value={email}
                                                    onChange={(e) => this.setState({ email: e.target.value })} />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-md-6 col-6 text-start pt-2">
                                            <input type="submit" className="btn btn-outline-info" disabled={buttonText !== 'Send Email'}
                                                value={buttonText}
                                                onClick={this.processLogin} />
                                        </div>
                                        <div className="col-md-6 col-6 text-end pt-2">
                                            <NavLink className="btn btn-outline-info text-success ms-3" to={ROUTE_END_POINTS.LOGIN} title="Login">
                                                Login
                                            </NavLink>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-12 text-start pt-2">
                                            <p className='text-success'>{message} </p>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
                {isLoading ? <Loader /> : null}
            </div>
        )
    }
}
export default ForgetPassword;
