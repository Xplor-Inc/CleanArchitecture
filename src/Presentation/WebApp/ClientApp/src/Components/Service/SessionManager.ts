import React from "react";
import { toast } from "react-toastify";
import { Service } from ".";
import { API_END_POINTS } from "../Core/Constants/EndPoints";
import { ROUTE_END_POINTS } from "../Core/Constants/RouteEndPoints";
import { IResult } from "../Core/Dto/IResultObject";

const _IsLogedIn = () => {
   let decodedCookie = decodeURIComponent(document.cookie).split(';');
   for (let q = 0; q < decodedCookie.length; q++) {
      const cookie = decodedCookie[q].split('=');
      if (cookie[0].trim() === "CA") {
         return true;
      }
   }
   return false;
}

const _LogOutUser = async (e: React.MouseEvent<HTMLAnchorElement>) => {

   e.preventDefault();

   var logoutResponse = await Service.Put<IResult<boolean>>(API_END_POINTS.LOGOUT, {});
   if (logoutResponse.hasErrors) {
      var errors = logoutResponse.errors;
      toast.error(errors[0])
   }
   else { 
      localStorage.removeItem('User');
   localStorage.removeItem('Image');
      toast.success(`Logged out successfully`);
      window.location.href = ROUTE_END_POINTS.HOME;
   }
}
const SessionManager = {
   IsLogedIn: _IsLogedIn,
   Logout: _LogOutUser
}
export default SessionManager;
