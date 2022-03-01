import { Route, Routes } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import Layout from './Pages/Components/Navigation/Layout';
import Home from './Pages/Home'
import Login from './Pages/Account/Login';
import ForgetPassword from './Pages/Account/ForgetPassword';
import { ROUTE_END_POINTS } from './Components/Core/Constants/RouteEndPoints';
import PasswordRecovery from './Pages/Account/PasswordRecovery';
import ChangePassword from './Pages/Account/ChangePassword';
import Users from './Pages/Users';
import About from './Pages/About';
import Contact from './Pages/Contact';
import Profile from './Pages/Profile';
import Enquiry from './Pages/Enquiry';
import { useSelector } from 'react-redux';

interface StateType {
    name : string
    image:string,
    isLoggedIn:boolean
}
const App = () => {
    const profile = useSelector<any, StateType>((state) => state.user)

    var isLogedIn = profile.isLoggedIn;
    return (
        <BrowserRouter>
            <Layout>
                <Routes>
                    {
                        isLogedIn ? //Only add routes if user is logged In.
                            <>
                                <Route path={ROUTE_END_POINTS.USERS} element={<Users />} />
                                <Route path={ROUTE_END_POINTS.CHANGE_PASSWORD} element={<ChangePassword />} />
                                <Route path={ROUTE_END_POINTS.USER_PROFILE} element={<Profile />} />
                                <Route path={ROUTE_END_POINTS.ENQUIRY} element={<Enquiry />} />
                            </>
                            : null}

                    <Route path={ROUTE_END_POINTS.LOGIN} element={<Login />} />
                    <Route path={ROUTE_END_POINTS.FORGET_PASSWORD} element={<ForgetPassword />} />
                    <Route path={ROUTE_END_POINTS.HOME} element={<Home />} />
                    <Route path={`${ROUTE_END_POINTS.PASSWORD_RECOVERY}/:Id/:Guid/:EmailAddress`} element={<PasswordRecovery />} />
                    <Route path={`${ROUTE_END_POINTS.ACCOUNTACTIVATION}/:Id/:Guid/:EmailAddress`} element={<PasswordRecovery />} />
                    <Route path='/about' element={<About />} />
                    <Route path='/contact' element={<Contact />} />

                </Routes>
            </Layout>
        </BrowserRouter>
    )
}

export default App;
