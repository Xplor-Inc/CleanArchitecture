import * as React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import SessionManager from '../../../Components/Service/SessionManager';
import { ROUTE_END_POINTS } from '../../../Components/Core/Constants/RouteEndPoints';
import NavDropdown from 'react-bootstrap/esm/NavDropdown';
import { FaSignOutAlt } from 'react-icons/fa';
import { ProfileImage } from './ProfileImage';
import { useAppSelector } from '../../../ReduxStore/hooks';

const NavMenu = () => {

    const isLogedIn = useAppSelector(state => state.user.isLoggedIn)

    const [isOpen, setIsOpen] = React.useState(false);
    const [activeClass, SetAciveClass] = React.useState(window.location.pathname);

    const toggle = () => {
        setIsOpen(!isOpen)
    }
    const handleClick = (path: string) => {
        if (isOpen) {
            toggle();
            SetAciveClass(path)
        }
        else
            SetAciveClass(path)
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm box-shadow fixed-top" light style={{ backgroundImage: "linear-gradient(to right, #81019f,#5a38af, #02cbab)" }}>
                <Container className="box-container">
                    <NavbarBrand tag={Link} to={ROUTE_END_POINTS.HOME} onClick={() => handleClick(ROUTE_END_POINTS.HOME)}>
                        <img src='/images/icon.jpeg' alt='Logo' />
                        Hoshiyar Singh</NavbarBrand>
                    <NavbarToggler onClick={toggle} className="mr-2" />
                    <Collapse className="navbar-collapse" isOpen={isOpen}>
                        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to={ROUTE_END_POINTS.HOME}
                                    active={activeClass === ROUTE_END_POINTS.HOME}
                                    onClick={() => handleClick(ROUTE_END_POINTS.HOME)}>
                                    Home
                                </NavLink>
                            </NavItem>
                            {
                                isLogedIn ?
                                    <>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to={ROUTE_END_POINTS.USERS}
                                                active={activeClass === ROUTE_END_POINTS.USERS}
                                                onClick={() => handleClick(ROUTE_END_POINTS.USERS)}>
                                                Users
                                            </NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to={ROUTE_END_POINTS.ENQUIRY}
                                                active={activeClass === ROUTE_END_POINTS.ENQUIRY}
                                                onClick={() => handleClick(ROUTE_END_POINTS.ENQUIRY)}>
                                                Enquiries
                                            </NavLink>
                                        </NavItem>
                                    </>
                                    : <>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/about"
                                                active={activeClass === "/about"}
                                                onClick={() => handleClick("/about")}>
                                                About
                                            </NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/contact"
                                                active={activeClass === "/contact"}
                                                onClick={() => handleClick("/contact")}>
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
                                            <NavDropdown title={<ProfileImage />} onClick={() => { setIsOpen(true)}}
                                                active={activeClass === ROUTE_END_POINTS.CHANGE_PASSWORD}>
                                                <Link className="dropdown-item" role={'button'} to={ROUTE_END_POINTS.CHANGE_PASSWORD}
                                                    onClick={() => handleClick(ROUTE_END_POINTS.CHANGE_PASSWORD)}>
                                                    Change Password
                                                </Link>
                                                <Link className="dropdown-item" role={'button'} to={ROUTE_END_POINTS.USER_PROFILE}
                                                    onClick={() => handleClick(ROUTE_END_POINTS.USER_PROFILE)}>
                                                    Update Profile
                                                </Link>
                                                <NavDropdown.Divider />
                                                <NavDropdown.Item href="/profile" onClick={SessionManager.Logout}>
                                                    Logout <FaSignOutAlt />
                                                </NavDropdown.Item>

                                            </NavDropdown>
                                        </>
                                        : <NavItem>
                                            <NavLink tag={Link} className="text-dark" to={ROUTE_END_POINTS.LOGIN}
                                                active={activeClass === ROUTE_END_POINTS.LOGIN}
                                                onClick={() => handleClick(ROUTE_END_POINTS.LOGIN)}>
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