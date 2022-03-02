import React from "react";
import ReactDOM from 'react-dom';

type ModelBox = {
    heading: string,
    message?: string,
    onClose?: React.MouseEventHandler<HTMLSpanElement>
    children?: React.ReactElement
}
const PopupWindow = (props: ModelBox) => {
    var element = document.getElementById('root-model') as HTMLElement;
    return ReactDOM.createPortal(
        <React.Fragment>
            <div className="text-left bg-white b-1 text-justify m-1 popup" id="validationError">
                <span className="closeX" id="X_Popup" onClick={props.onClose}>X</span>
                <div className='heading p-2 pb-0'>
                    <h4>
                        {props.heading}
                    </h4>
                </div>
                <div className="p-3 pt-0">
                    {props.message ? props.message : ''}
                    {props.children}
                </div>
            </div>
            <div className="overlay"></div>
        </React.Fragment>,
        element
    )
}

export default PopupWindow;