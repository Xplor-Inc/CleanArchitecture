import React from "react"
import { GrPrevious, GrNext } from "react-icons/gr"
import { IPagingResult } from "../../Components/Core/Dto/Paging"

export class Paging extends React.Component<IPagingResult, {}> {

    render() {
        const { text, nextDisabled, prevDisabled, prevPage, nextPage } = this.props
        return (
            text.count > 0 ?
                <>
                    <label style={{ fontSize: '1rem' }}>
                        <strong> {text.from} </strong>
                        -<strong> {text.to} </strong>
                        of <strong>{text.count}</strong>
                    </label> &nbsp;
                    {
                        !nextDisabled || !prevDisabled ?
                            <React.Fragment>
                                <button type="button" value="<" className="paging" onClick={prevPage}
                                    disabled={prevDisabled} title='Newer'> <GrPrevious /></button>
                                <button type="button" className="paging"
                                    disabled={nextDisabled} title='Older' value=">" onClick={nextPage} >
                                    <GrNext />
                                </button>
                            </React.Fragment>
                            : ''
                    }
                </>
                : ''
        )
    }
}
