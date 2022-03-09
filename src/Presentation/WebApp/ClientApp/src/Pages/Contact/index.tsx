import PageTitle from "../Components/Navigation/PageTitle";
import ContactDetails from "./ContactDetails";
import ContactForm from "./ContactForm";

const Contact = () => {

    return (
        <div className="lightbox-wrapper lightbox-content container-body">
            <PageTitle title="Contact Us" />
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
                        <ContactForm />
                    </div>
                    <div className="col-12 col-lg-5">
                        <ContactDetails/>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Contact;