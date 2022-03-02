import { FaKey } from 'react-icons/fa'
import React, { useEffect, useState } from 'react'
import { Service } from '../../Components/Service'
import { API_END_POINTS } from '../../Components/Core/Constants/EndPoints'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import PageTitle from '../Components/Navigation/PageTitle';
import { ToastError } from '../Components/ToastError';
import { NavLink, useParams } from 'react-router-dom';
import { ROUTE_END_POINTS } from '../../Components/Core/Constants/RouteEndPoints';
import { Loader } from '../Components/Loader';
import { IResult } from '../../Components/Core/Dto/IResultObject';


const PasswordRecovery = () => {

    const { EmailAddress, Guid, Id } = useParams()
    const [newPassword, SetNewPassword] = useState('')
    const [confirmPassword, SetConfirmPassword] = useState('')
    const [isValid, SetIsValid] = useState(false)
    const [isLoading, SetIsLoading] = useState(true)

    var path = "";
    if (window.location.pathname.includes('/reset-password/'))
        path = API_END_POINTS.RESET_PASSWORD;
    else if (window.location.pathname.includes('/account-activation/'))
        path = API_END_POINTS.ACCOUNTACTIVATION;

    else {
        window.location.href = ROUTE_END_POINTS.HOME;
    }

    useEffect(() => {

        var body = {
            resetlink: `${Id}/${Guid}/${EmailAddress}`,
            emailAddress: EmailAddress
        }
        Service.Post<IResult<boolean>>(path, body)
            .then(response => {
                if (response.hasErrors) {
                    toast.error(<ToastError errors={response.errors} />)
                    SetIsLoading(false);
                    return;
                }
                SetIsLoading(false)
                SetIsValid(true)
            })
    }, [EmailAddress, Guid, Id])

    const changePassword = async (e: React.MouseEvent<HTMLInputElement>) => {
        e.preventDefault();
        var errors = [];
        if (newPassword.length === 0) {
            errors.push('Please enter new password');
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

        SetIsLoading(true);

        var body = {
            resetlink: `${Id}/${Guid}/${EmailAddress}`,
            password: newPassword,
            emailAddress: EmailAddress
        }
        Service.Put<IResult<boolean>>(path, body).then(response => {

            if (response.hasErrors) {
                toast.error(<ToastError errors={response.errors} />)
                SetIsLoading(false);
                return;
            }
            toast.success(<ToastError errors={["Password reset successfully"]} />)
            window.location.href = ROUTE_END_POINTS.LOGIN;
        })

    }

    return (
        <div className="row">
            {isLoading ? <Loader /> : null}
            <PageTitle title='Password Recovery' />
            <div className="col-md-3">
            </div>
            <div className="col-md-6">
                <div className="card mb-3 mt-4">
                    <div id="per86" className="collapse show">
                        <div className="card-body">
                            <form method="post" action="/account/login">
                                <div className="row">
                                    <div className="col-md-12">
                                        <h2>Reset Password</h2>
                                        <hr className="mt-0 pt-0" />
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col-md-12">
                                        Email Address :  <label>{EmailAddress}</label>
                                        <br /> <br />
                                    </div>
                                </div>
                                {isValid ?
                                    <>
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
                                                        onChange={(e) => SetNewPassword(e.target.value)} />
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
                                                        onChange={(e) => SetConfirmPassword(e.target.value)} />
                                                </div>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div className="col-md-6 col-6 text-right pt-2">
                                                <input type="submit" className="btn btn-outline-info" disabled={isLoading}
                                                    value={isLoading ? "Loading..." : "Reset Password"}
                                                    onClick={changePassword} />
                                            </div>
                                            <div className="col-md-6 col-6 text-right pt-2">
                                                <NavLink className="btn btn-outline-info" to={ROUTE_END_POINTS.LOGIN} title="Login">
                                                    Login
                                                </NavLink>
                                            </div>
                                        </div>
                                    </> : null
                                }
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            {isLoading ? <Loader /> : null}
        </div>
    )
}

export default PasswordRecovery;
