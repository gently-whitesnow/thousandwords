import React from "react";
import { observer } from "mobx-react-lite";
import { WinContentWrapper } from "./WinContent.styles";

const WinContent = (props) => {
  return (
    <WinContentWrapper onClick={props.onClick}>
      <div>Поздравляю, ты выучил 1000 слов!</div>
      <div>Cказать спасибо можно тут:</div>
      <a href="https://t.me/gently_whitesnow" target="_blank" >Telegram</a>
    </WinContentWrapper>
  );
};

export default observer(WinContent);
