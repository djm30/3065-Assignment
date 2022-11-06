import React from "react";

interface Props {
  className: string;
  children: React.ReactNode;
}

const Container = ({ className, children }: Props) => {
  return (
    <div className={`xl:mx-60 lg:mx-40 md:mx-24 ${className}`}>{children}</div>
  );
};

export default Container;
