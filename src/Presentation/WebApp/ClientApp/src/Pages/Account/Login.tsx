import { FaEnvelope, FaKey } from 'react-icons/fa'
import React, { useState } from 'react'
import { Service } from '../../Components/Service'
import { API_END_POINTS } from '../../Components/Core/Constants/EndPoints'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import PageTitle from '../Components/Navigation/PageTitle';
import { ToastError } from '../Components/ToastError';
import { NavLink } from 'react-router-dom';
import { ROUTE_END_POINTS } from '../../Components/Core/Constants/RouteEndPoints';
import { Loader } from '../Components/Loader';
import { Utility } from '../../Components/Service/Utility';
import { IResult } from '../../Components/Core/Dto/IResultObject';
import { IUserDto } from '../../Components/Core/Dto/Users';
import { updateProfileState } from '../../ReduxStore/userProfileSlice';
import { useNavigate } from "react-router-dom";
import { useAppDispatch } from '../../ReduxStore/hooks';

const Login = () => {
    const [loginData, SetLoginData] = useState<{ email: string, password: string, isLoading: boolean }>({ email: '', isLoading: false, password: '' })
    let navigate = useNavigate();
    const dispatch = useAppDispatch()
    
    const processLogin = async (e: React.MouseEvent<HTMLInputElement>) => {
        e.preventDefault();
        var errors = [];
        if (!Utility.Validate.String(loginData.email)) {
            errors.push('Please enter email address');
        }
        if (loginData.password.length === 0) {
            errors.push('Please enter password');
        }
        if (errors.length > 0) {
            toast.error(<ToastError errors={errors} />);
            return;
        }
        SetLoginData({ ...loginData, isLoading: true })
        var body = {
            password: loginData.password,
            emailAddress: loginData.email
        }
        var response = await Service.Post<IResult<IUserDto>>(API_END_POINTS.LOGIN, body);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            SetLoginData({ ...loginData, isLoading: false })
            return;
        }
        if (response.resultObject) {
            dispatch(updateProfileState({ name: response.resultObject.firstName, 
                image: response.resultObject.imagePath,
                isLoggedIn:true
             }))
            SetLoginData({ ...loginData, isLoading: false })
            navigate(`/`)
        }
    }
  
        const { isLoading, email, password } = loginData
        return (
            <div className="row">
                <PageTitle title='Login' />
                <div className="col-md-3">
                </div>
                <div className="col-md-6">
                    <div className="card mb-3 mt-4">
                        <div id="per86" className="collapse show">
                            <div className="card-body">
                                <form method="post" action="/account/login">
                                    <div className="row">
                                        <div className="col-md-12">
                                            <h2>Login</h2>
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
                                                    onChange={(e) => SetLoginData({ ...loginData, email: e.target.value }) } />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-md-12">
                                            <label>Password</label>
                                            <div className="input-group">
                                                <div className="input-group-prepend">
                                                    <span className="input-group-text">
                                                        <FaKey style={{ height: '1.5rem' }} />
                                                    </span>
                                                </div>
                                                <input type="password" className="form-control" placeholder="Enter Password"
                                                    value={password}
                                                    onChange={(e) => SetLoginData({ ...loginData, password: e.target.value })} />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-md-6 col-6 text-start pt-2">
                                            <input type="submit" className="btn btn-outline-info" disabled={isLoading}
                                                value={isLoading ? "Loading..." : "Login"}
                                                onClick={processLogin} />
                                        </div>
                                        <div className="col-md-6 col-6 text-end pt-2">
                                            <NavLink className="btn btn-outline-warning" to={ROUTE_END_POINTS.FORGET_PASSWORD} title="Forget Password">
                                                Forget Password
                                            </NavLink>
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
    
export default Login;
