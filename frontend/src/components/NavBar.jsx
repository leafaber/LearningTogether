import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import ProfileEdit from './ProfileEdit';

export default function NavBar({ isLoggedIn }) {
  const [profileEditMenuState, setProfileEditMenuState] = useState(false);

  const closeMenu = () => {
    setProfileEditMenuState(false);
  }

  return (
    <>
      {
        profileEditMenuState ?
        <div className='flex justify-center'>
          <ProfileEdit closeMenu={closeMenu} />
        </div>
        :
          null
      }
      <header className='flex items-center min-[782px]:justify-between justify-center bg-white border-b border-black'>
        <div className='flex items-center gap-7'>
          <Link to='/'>
            <div className='bg-white text-xl font-extrabold text-center px-4 py-4 min-[782px]:border-r border-black border-dashed min-w-max'>LEARNING TOGETHER
              <p className='text-xs font-normal'>WHERE THE WORLD LEARNS</p>
            </div>
          </Link>
          <div className='hidden min-[782px]:visible min-[782px]:flex justify-between items-center'>
            <div className='flex flex-row flex-nowrap justify-between'> 
              <Link to='/courses'>
                <div className='bg-white text-xl underline decoration-1 font-semibold px-1.5 py-3 pr-10'>Courses</div>
              </Link>
              <Link to='/categories'>
                <div className='bg-white text-xl underline decoration-1 font-semibold px-1.5 py-3 pr-7'>Categories</div>
              </Link>
            </div>
          </div>
        </div>
        <div className='hidden min-[782px]:visible min-[782px]:flex flex-row flex-nowrap justify-around border-l border-black border-dashed px-2 p-1 gap-5'> 
        {
          isLoggedIn ?
            <>
              <Link to='/'>
                <div className='bg-white text-xl py-2 px-3 ml-4 my-3 border border-black font-semibold min-w-max'>Log in</div>
              </Link>
              <Link to='/'>
                <div className='bg-white text-xl py-2 px-3 mr-4 my-3 border border-black font-semibold'>Register</div>
              </Link>
            </>
          :
            <>
              <Link>
                <div className='bg-white text-xl py-2 px-3 ml-4 my-3 border border-black font-semibold'
                    onClick={() => setProfileEditMenuState(!profileEditMenuState)}
                >
                  Profile
                </div>
              </Link>
              <Link to='/' onClick={() => { window.sessionStorage.removeItem('token')}}>
                <div className='bg-white text-xl py-2 px-3 mr-4 my-3 border border-black font-semibold'>Log out</div>
              </Link>
            </>
        }
        </div>
      </header>
    </>
  )
}
