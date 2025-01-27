import React from "react";
import HeroPicture from "./HeroPicture";
import HeroText from "./HeroText";

/*********************************************************
 | Rev: 1.0 - created the component | Author: Mihael | 

 *********************************************************/

export default function Hero({ handleLogIn }) {
  return (
    <div
      className="flex flex-col justify-evenly min-w-max md:flex-row items-center gap-y-5
                        border-black border-2 rounded-2xl p-5 sm:p-10"
    >
      <HeroText handleLogIn={handleLogIn} />
    </div>
  );
}
