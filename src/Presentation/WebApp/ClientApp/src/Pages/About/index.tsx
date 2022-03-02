import React from "react";
import PageTitle from "../Components/Navigation/PageTitle";

const About = () => {

    return <>
        <PageTitle title='About' />
        <div className='text-center' style={{ paddingTop: "10vh", color: 'wheat' }}>
            <h2>
                Hey, It's about Hoshiyar Singh
            </h2>
            <h3>He is an Engineer</h3>
            <h4>He is a Programmer</h4>
            <h5>..................</h5>
            <h4>..................</h4>
            <h3>..................</h3>
            <h2>He is Developer</h2>
        </div>
    </>
}

export default About;