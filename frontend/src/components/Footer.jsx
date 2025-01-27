import React from 'react';
import SocialMedia from './SocialMedia';


export default function Footer() {
  return (
    <footer className='bg-orange-400 text-black' >
      <div className='md:flex md:justify-between md:items-center sm:px-12 px-5 bg-orange-300 py-3'>
        <h1 className='lg:text-4xl text-3xl sm:text-2xl md:mb-0 mb-6 lg:leading-normal font-sans md:w-2/5 sm:w-1/4'> 
          <span className='text-gray-900 font-medium'> Learning </span> Together <br/> 
          <span className='lg:text-xl text-lg sm:text-base italic'> ©2022 All rights reserved.</span>
        </h1>
        <SocialMedia />
      </div>

    </footer>
  );
}
