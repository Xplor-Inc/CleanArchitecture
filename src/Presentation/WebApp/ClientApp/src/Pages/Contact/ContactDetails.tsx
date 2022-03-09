import { FiMapPin, MdMailOutline, MdCall } from 'react-icons/all'

const ContactDetails = () => {
    
    return (
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
                            <MdMailOutline />
                        </span>
                        <div className="media-body info-details">
                            <h6 className="info-type">Email Me</h6>
                            <span className="info-value">
                                <a href="mailto:connect@app.xyz">connect@app.xyz</a>
                            </span>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
    )
}

export default ContactDetails;