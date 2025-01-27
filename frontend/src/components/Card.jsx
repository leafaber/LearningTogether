import React from "react";

export default function Card(props) {

  return (
    <div className="border-black border-2 w-[150px] h-[140px] rounded-xl flex flex-none flex-col
                    items-center sm:w-52 sm:h-52 lg:w-60 m-5 overflow-hidden cursor-pointer">
      <img className="" src="https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg" />
      <p className="mt-2 sm:mt-4 font-bold">{props.title}</p>
    </div>
  );
}