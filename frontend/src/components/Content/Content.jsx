import React from "react";
import { observer } from "mobx-react-lite";
import { ContentWrapper } from "./Content.styles";
import CardHolder from "../CardHolder";

const Content = () => {
  return (
    <ContentWrapper>
      <CardHolder />
    </ContentWrapper>
  );
};

export default observer(Content);
