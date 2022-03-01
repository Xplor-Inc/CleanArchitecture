import React, { useState } from "react";
import { FiMapPin, MdOutlineMail, MdCall } from 'react-icons/all'
import { toast } from "react-toastify";
import { API_END_POINTS } from "../../Components/Core/Constants/EndPoints";
import { IResult } from "../../Components/Core/Dto/IResultObject";
import { Service } from "../../Components/Service";
import { Loader } from "../Components/Loader";
import PageTitle from "../Components/Navigation/PageTitle";
import PopupWindow from "../Components/PopupWindow";
import { ToastError } from "../Components/ToastError";

const Contact = () => {
    const [isLoading, SetIsLoading] = useState(false)
    const [buttonText, SetButtonText] = useState('Send Message')
    const [email, SetEmail] = useState('')
    const [message, SetMessage] = useState('')
    const [name, SetName] = useState('')
    const [subject, SetSubject] = useState('')

    const sendData = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        var errors = [];
        if (name.length === 0) {
            errors.push('Please Enter Name');
        }
        if (email.length === 0) {
            errors.push('Please Enter Email');
        }
        if (subject.length === 0) {
            errors.push('Please Enter Subject');
        }
        if (message.length === 0) {
            errors.push('Please Enter Message');
        }
        if (errors.length > 0) {
            toast.error(<ToastError errors={errors} />)
            return;
        }
        SetButtonText('Sending...')
        SetIsLoading(true);
        var dto = {
            email: email,
            message: message,
            name: name,
            subject: subject
        }
        var result = await Service.Post<IResult<boolean>>(API_END_POINTS.ENQUIRY, dto);
        if (result.hasErrors) {
            toast.error(<ToastError errors={result.errors} />)
            SetButtonText('Send Message')
            SetIsLoading(false);
            return;
        }
        setTimeout(() => {
            SetButtonText('Send Message')
        }, 1000 * 5);
        SetButtonText('Sent')
        SetIsLoading(false);
        SetName('')
        SetEmail('')
        SetSubject('')
        SetMessage('')
    }
    return (
        <div className="lightbox-wrapper lightbox-content container-body">
            <PageTitle title="Contact Us" />
            {isLoading ? <Loader /> : null}
            <div className="row">
                <div className="col-12">
                    <div className="section-heading page-heading">
                        <h2 className="section-title">Get in Touch</h2>
                        <div className="animated-bar"></div>
                    </div>
                </div>
            </div>
            <div className="contact-section single-section">
                <div className="row">
                    <div className="col-12 col-lg-7">
                        <form className="contact-form" id="contact-form" action="php/contact.php">
                            <h4 className="content-title">Message Me</h4>
                            <div className="row">
                                <div className="col-12 col-md-6 form-group">
                                    <input className="form-control" type="text" placeholder="Name" required
                                        value={name} onChange={(e) => SetName(e.currentTarget.value)} />
                                </div>
                                <div className="col-12 col-md-6 form-group">
                                    <input className="form-control" type="email" name="email" placeholder="Email" required
                                        value={email} onChange={(e) => SetEmail(e.currentTarget.value)} />
                                </div>
                                <div className="col-12 form-group">
                                    <input className="form-control" type="text" name="subject" placeholder="Subject" required
                                        value={subject} onChange={(e) => SetSubject(e.currentTarget.value)} />
                                </div>
                                <div className="col-12 form-group form-message">
                                    <textarea className="form-control" name="message" placeholder="Message" rows={5} required
                                        value={message} onChange={(e) => SetMessage(e.currentTarget.value)} ></textarea>
                                </div>
                                <div className="col-12 form-submit">
                                    <button className="btn button-main button-scheme" id="contact-submit" type="submit"
                                        disabled={buttonText !== "Send Message"} onClick={sendData}
                                        style={{ backgroundColor: buttonText !== "Send Message" ? "Grey" : "" }}>
                                        {buttonText}</button>
                                    <div className="ms-3 contact-feedback">
                                        {
                                            buttonText === "Sent" ?
                                                <PopupWindow heading={`Thanks ${name} for contacting us!`} onClose={() => {
                                                    SetButtonText('Send Message'); 
                                                }}>
                                                    <div>You are very important to us, all information received will always remain confidential.
                                                        We will contact you as soon as we review your message.
                                                    </div>
                                                </PopupWindow> : ""
                                        }
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div className="col-12 col-lg-5">
                        <div className="contact-info">
                            <h4 className="content-title">Contact Info</h4>
                            <p className="info-description">Always available for freelance work if the right project comes along, Feel free to contact me!</p>
                            <ul className="list-unstyled list-info">
                                <li>
                                    <div className="media align-items-center">
                                        <span className="info-icon">
                                            <FiMapPin />
                                        </span>
                                        <div className="media-body info-details">
                                            <h6 className="info-type">Name</h6><span className="info-value">Hoshiyar Singh</span>
                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <div className="media align-items-center"><span className="info-icon">
                                        <FiMapPin />
                                    </span>
                                        <div className="media-body info-details">
                                            <h6 className="info-type">Location</h6><span className="info-value">Nawalgarh, Rajasthan.</span>
                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <div className="media align-items-center">
                                        <span className="info-icon">
                                            <MdCall />
                                        </span>
                                        <div className="media-body info-details">
                                            <h6 className="info-type">Call Me</h6>
                                            <span className="info-value">
                                                <a href="tel:+919024236927">+91 14942 36927</a></span>
                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <div className="media align-items-center">
                                        <span className="info-icon">
                                            <MdOutlineMail />
                                        </span>
                                        <div className="media-body info-details">
                                            <h6 className="info-type">Email Me</h6>
                                            <span className="info-value">
                                                <a style={{}} href="mailto:connect@app.xyz">connect@app.xyz</a>
                                            </span>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Contact;