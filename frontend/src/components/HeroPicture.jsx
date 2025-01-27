import React from 'react';
import heroImage from '../../public/images/heroImage.jpeg';

export default function HeroPicture() {
    return (
        <img 
            className="sm:absolute sm:top-[1%] sm:left-[55%] sm:z-50 sm:h-[60%]
                       sm:w-4/12 border-2 border-black rounded-2xl"
            src={heroImage}
            alt="Thou shall be inspired"
        />
    );
}