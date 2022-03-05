import * as React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem } from 'reactstrap';
import { Link, NavLink } from 'react-router-dom';
import SessionManager from '../../../Components/Service/SessionManager';
import { ROUTE_END_POINTS } from '../../../Components/Core/Constants/RouteEndPoints';
import NavDropdown from 'react-bootstrap/esm/NavDropdown';
import { FaSignOutAlt } from 'react-icons/fa';
import { ProfileImage } from './ProfileImage';
import { useAppSelector } from '../../../ReduxStore/hooks';

const NavMenu = () => {

    const isLogedIn = useAppSelector(state => state.user.isLoggedIn)

    const [isOpen, setIsOpen] = React.useState(false);

    const toggle = () => {
        setIsOpen(!isOpen)
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm box-shadow fixed-top" light style={{ backgroundImage: "linear-gradient(to right, #81019f,#5a38af, #02cbab)" }}>
                <Container className="box-container">
                    <NavbarBrand tag={Link} to={ROUTE_END_POINTS.HOME}>
                        <img src='/images/icon.jpeg' alt='Logo' />
                    </NavbarBrand>
                    <NavbarToggler onClick={toggle} className="mr-2" />
                    <Collapse className="navbar-collapse" isOpen={isOpen}>
                        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                            {
                                isLogedIn ?
                                    <>
                                        <NavItem>
                                            <NavLink className="text-dark nav-link" to={ROUTE_END_POINTS.USERS}>
                                                Users
                                            </NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink className="text-dark nav-link" to={ROUTE_END_POINTS.ENQUIRY}>
                                                Enquiries
                                            </NavLink>
                                        </NavItem>
                                    </>
                                    : <>
                                        <NavItem>
                                            <NavLink className="text-dark nav-link" to={ROUTE_END_POINTS.HOME}>
                                                Home
                                            </NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink className="text-dark nav-link" to="/about">
                                                About
                                            </NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink className="text-dark nav-link" to="/contact">
                                                Contact
                                            </NavLink>
                                        </NavItem>
                                    </>
                            }
                        </ul>
                        <div className="d-flex">
                            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                                {
                                    isLogedIn ?
                                        <>
                                            <NavDropdown title={<ProfileImage />}>
                                                <NavDropdown.Item className="dropdown-item" role={'button'}>
                                                    <NavLink className="dropdown-item" to={ROUTE_END_POINTS.CHANGE_PASSWORD}>
                                                        Change Password
                                                    </NavLink>
                                                </NavDropdown.Item>
                                                <NavDropdown.Item className="dropdown-item" role={'button'}>
                                                    <NavLink className="dropdown-item" role={'button'} to={ROUTE_END_POINTS.USER_PROFILE}>
                                                        Update Profile
                                                    </NavLink>
                                                </NavDropdown.Item>
                                                <NavDropdown.Divider />
                                                <NavDropdown.Item href="/profile" onClick={SessionManager.Logout}>
                                                    <NavLink className="dropdown-item" role={'button'} to="#logout">
                                                        Logout <FaSignOutAlt />
                                                    </NavLink>
                                                </NavDropdown.Item>

                                            </NavDropdown>
                                        </>
                                        : <NavItem>
                                            <NavLink className="text-dark nav-link" to={ROUTE_END_POINTS.LOGIN}>
                                                Login
                                            </NavLink>
                                        </NavItem>
                                }
                            </ul>
                        </div>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
}

export default NavMenu;