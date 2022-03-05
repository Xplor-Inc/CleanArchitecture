import React from "react";
import { Service } from ".";
import { API_END_POINTS } from "../Core/Constants/EndPoints";
import { ROUTE_END_POINTS } from "../Core/Constants/RouteEndPoints";
import { IResult } from "../Core/Dto/IResultObject";


const _LogOutUser = async (e: React.MouseEvent<HTMLAnchorElement>) => {

    e.preventDefault();

    await Service.Put<IResult<boolean>>(API_END_POINTS.LOGOUT, {});

    window.document.cookie = "CA=CA; expires=2020-03-06T03:07:21.616Z";
    localStorage.removeItem('profile');
    window.location.href = ROUTE_END_POINTS.HOME;
}
const SessionManager = {
    Logout: _LogOutUser
}
export default SessionManager;
