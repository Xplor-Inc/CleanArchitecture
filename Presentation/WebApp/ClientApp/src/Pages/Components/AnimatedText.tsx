import React from "react"
import { memo, useEffect, useState } from "react"
import { FaPen } from "react-icons/fa"

var words = ['Professional Coder', 'Programmer', 'Designor', 'Developer'],
    part,
    i = 0,
    offset = 0,
    len = words.length,
    forwards = true,
    skip_count = 0,
    skip_delay = 15

const AnimatedText = () => {
    const [text, SetText] = useState('')
    useEffect(() => {
      var intervalId =  setInterval(function () {
            if (forwards) {
                if (offset >= words[i].length) {
                    ++skip_count;
                    if (skip_count == skip_delay) {
                        forwards = false;
                        skip_count = 0;
                    }
                }
            }
            else {
                if (offset == 0) {
                    forwards = true;
                    i++;
                    offset = 0;
                    if (i >= len) {
                        i = 0;
                    }
                }
            }
            part = words[i].substring(0, offset);
            if (skip_count == 0) {
                if (forwards) {
                    offset++;
                }
                else {
                    offset--;
                }
            }
            SetText("a " + part)
      }, 100 * 1);
        return () => { clearInterval(intervalId) };
    }, [words])
    return (
        <div id="animate" className='word'>
            {text} <FaPen color="white" fontSize={"3rem"}/>
        </div>
    )
}
export default memo(AnimatedText);