import React from 'react';
import { Link, Outlet } from 'react-router-dom';
import ProfileEdit from './ProfileEdit';

export default function MobileMenu({ current, isLoggedIn }) {
    const [menuState, setMenuState] = React.useState(false);
    const [profileMenuEditState, setProfileEditMenuState] = React.useState(false);

    const setMenu = () => {
      setMenuState((prevMenuState) => !prevMenuState);
    };

    const closeMenu = () => {
        setProfileEditMenuState(false);
    }

    return (
        <>
            {
                profileMenuEditState ?
                    <div className="flex justify-center">
                        <ProfileEdit closeMenu={closeMenu} />
                    </div>
                :
                    null
            }
            {
                menuState === false ?
                (
                    <div className="fixed bottom-2 flex justify-between z-40 bg-white border border-black">
                        <div className="flex justify-center">
                            <span className="border-r-[0.1px] border-dashed border-black px-4 py-2 font-nunito">{current}</span>
                        </div>
                        <div 
                            className="flex flex-col justify-around p-3 align-middle cursor-pointer"
                            onClick={setMenu}
                        >
                            <div className="w-5 h-px bg-black"></div>
                            <div className="w-5 h-px bg-black"></div>
                        </div>
                    </div>
                )
                :
                (
                    <>
                        <div className="fixed w-[92.5%] h-[55%] z-40 bottom-[3.33rem] bg-white border border-black transition-all duration-300">
                            <div className="absolute right-[10%] h-[100%] w-px border-r border-dashed border-black"></div>
                            <div className="absolute bottom-0 h-[97%] right-[9.3%] flex flex-col justify-evenly">
                                {
                                    !isLoggedIn ?
                                        <>
                                            <Link to="/" className="flex justify-end items-center">
                                                <h1 className="mr-5">Register</h1>
                                                <span className="h-px w-20 border-t border-dashed border-black"></span>
                                                <span className="h-2 w-2 bg-black rounded-[50%]"></span>
                                            </Link>
                                            <Link to="/" className="flex justify-end items-center">
                                                <h1 className="mr-5">Login</h1>
                                                <span className="h-px w-20 border-t border-dashed border-black"></span>
                                                <span className="h-2 w-2 bg-black rounded-[50%]"></span>
                                            </Link>
                                        </>
                                    :
                                        <>
                                            <Link className="flex justify-end items-center"
                                                  onClick={() => setProfileEditMenuState(!profileMenuEditState)}
                                            >
                                                <h1 className="mr-5">Profile</h1>
                                                <span className="h-px w-20 border-t border-dashed border-black"></span>
                                                <span className="h-2 w-2 bg-black rounded-[50%]"></span>
                                            </Link>
                                            <Link to="/"
                                                className="flex justify-end items-center"
                                                onClick={() => window.sessionStorage.removeItem('token')}
                                            >
                                                <h1 className="mr-5">Log out</h1>
                                                <span className="h-px w-20 border-t border-dashed border-black"></span>
                                                <span className="h-2 w-2 bg-black rounded-[50%]"></span>
                                            </Link>
                                        </>
                                } 
                                <Link to="/categories" className="flex justify-end items-center">
                                    <h1 className="mr-5">Categories</h1>
                                    <span className="h-px w-20 border-t border-dashed border-black"></span>
                                    <span className="h-2 w-2 bg-black rounded-[50%]"></span>
                                </Link>
                                {
                                /*
                                *  Should be rendered conditionaly if the user is not logged in, then these appear
                                *  Should also add Log out if the user is logged in
                                */
                                }
                                <Link to="/courses" className="flex justify-end items-center">
                                    <h1 className="mr-5">Courses</h1>
                                    <span className="h-px w-20 border-t border-dashed border-black"></span>
                                    <span className="h-2 w-2 bg-black rounded-[50%]"></span>
                                </Link>
                                <Link to="/" className="flex justify-end items-center">
                                    <h1 className="mr-5">Home</h1>
                                    <span className="h-px w-20 border-t border-dashed border-black"></span>
                                    <span className="h-2 w-2 bg-black rounded-[50%]"></span>
                                </Link>
                            </div>
                        </div>
                        <div className="fixed w-[92.5%] bottom-2 grid grid-flow-col grid-cols-2 z-50 bg-white border border-black">
                            <div className="flex justify-center align-middle border-r-[0.1px] border-dashed border-black">
                                <span className="px-4 py-2 font-nunito">{current}</span>
                            </div>
                            {/*<div className="border-r-[0.1px] border-dashed border-black"></div>*/}
                            <div className="flex justify-center cursor-pointer"
                                onClick={setMenu}
                            >
                                <div 
                                    className="flex flex-col justify-around p-3 align-middle"
                                >
                                    <div className="w-5 h-[0.5px] bg-black rotate-45 translate-y-1"></div>
                                    <div className="w-5 h-[0.5px] bg-black -rotate-45 -translate-y-1"></div>
                                </div>
                            </div>
                        </div>
                    </>
                )
            }
        </>
    )
}