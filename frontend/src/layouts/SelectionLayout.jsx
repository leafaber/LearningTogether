import React from "react";

export default function SelectionLayout({ children }) {
  return (
    <div
      className="flex flex-col justify-evenly min-w-max md:flex-row gap-y-5
                        border-black border-2 rounded-2xl p-5 sm:p-10"
    >
      <div className="flex flex-col gap-y-1">{children}</div>
    </div>
  );
}
