import React from "react";

export default function Card(props) {

  return (
    <div className="border-zinc-700 border-r h-auto flex flex-none flex-col items-start w-full sm:w-1/3 mt-3 pl-3 overflow-hidden">
      <p className="mt-2 sm:mt-4 font-bold text-xl">{props.title}</p>
      <p className="mt-2 sm:mt-4 font-medium">{props.description}</p>
           
      <div className="font-medium pt-5 pb-3 self-center">
        <ul className="list-disc">
          <li><p>Material 1 <a href="/">Material 1</a> </p></li>
          <li><p>Material 2 <a href="/">Material 2</a> </p></li>
          <li><p>Material 3 <a href="/">Material 3</a> </p></li>
        </ul>
      </div>      
    </div>  
  );
}
