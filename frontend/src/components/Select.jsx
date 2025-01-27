import React from "react";

export default function Select({
  options,
  placeholder,
  title,
  onChange,
  selectedValue,
}) {
  return (
    <div className="flex flex-col justify-center">
      <div className="font-bold text-xl sm:text-3xl text-center m-5">
        {title}
      </div>
      <div className="mb-3 w-full">
        <select
          className="form-select appearance-none
          border-gray-500 
          border-2 border-radius rounded p-1
      block
      w-full
      px-3
      py-1.5
      text-base
      font-normal
      text-gray-700
      bg-white bg-clip-padding bg-no-repeat
      border-solid

      transition
      ease-in-out
      m-0
      focus:text-gray-700 focus:bg-white focus:border-blue-600 focus:outline-none"
          disabled={options.length < 1}
          onChange={onChange}
          value={selectedValue}
        >
          <option value={"0"}>{placeholder}</option>
          {options.map((opt) => (
            <option value={`${opt.value}`}>{opt.label}</option>
          ))}
        </select>
      </div>
    </div>
  );
}
