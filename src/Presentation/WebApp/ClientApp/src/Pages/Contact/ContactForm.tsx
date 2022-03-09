import React, { useState } from "react";
import { toast } from "react-toastify";
import { API_END_POINTS } from "../../Components/Core/Constants/EndPoints";
import { IResult } from "../../Components/Core/Dto/IResultObject";
import { Service } from "../../Components/Service";
import { Loader } from "../Components/Loader";
import PopupWindow from "../Components/PopupWindow";
import { ToastError } from "../Components/ToastError";

const ContactForm = () => {
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
        <form className="contact-form" id="contact-form" action="php/contact.php">
            {isLoading ? <Loader /> : null}
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
    )
}

export default ContactForm;