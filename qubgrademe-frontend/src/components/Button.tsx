import React, { Children } from "react";

interface Props {
    className?: string;
    children: React.ReactNode;
    onClick: () => void;
}

const Button = ({ className, children, onClick }: Props) => {
    return (
        <button
            onClick={onClick}
            className={`${
                className?.includes("bg")
                    ? ""
                    : "bg-accentBlue hover:bg-accentHoverBlue"
            } text-white rounded-md px-4 py-3 transition-all ${className}`}
        >
            {children}
        </button>
    );
};

export default Button;
