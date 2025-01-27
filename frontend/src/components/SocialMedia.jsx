import React from 'react'
import Instagram from '../../public/images/Instagram.png';
import Facebook from '../../public/images/Facebook.png';
import Twitter from '../../public/images/Twitter.png'

export default function SocialMedia() {
  return (
    <>
      <div className='inline-flex items-center'>
          <a href='https://www.instagram.com' target='_blank'> 
            <img src={Instagram} alt='Instagram' className='cursor-pointer rounded-full w-14 h-14 mx-2 hover:scale-110' ></img>
          </a>
          <a href='https://www.twitter.com' target='_blank'>
            <img src={Twitter} alt='Twitter' className='cursor-pointer rounded-full w-14 h-14 mx-2 hover:scale-110' ></img>
          </a>
          <a href='https://www.facebook.com' target='_blank'>
            <img src={Facebook} alt="Facebook" className='cursor-pointer rounded-full w-14 h-14 mx-2 hover:scale-110' ></img>
          </a>
      </div>
    </>
  )
}
