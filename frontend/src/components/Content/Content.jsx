import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { ContentWrapper } from "./Content.styles";
import CardHolder from "../CardHolder";
import LevelBar from "../LevelBar";
import LevelNumber from "../LevelNumber";
import { useStore } from "../../store";
import MindWord from "../MindWord";

const Content = () => {
  const { wordsStore } = useStore();
  const { getWord, getOtherWords, currentWord, isNative, level, setIsNative } =
    wordsStore;

  return (
    <ContentWrapper>
      <LevelNumber level={level} />
      <LevelBar />
      <MindWord
        word={isNative ? currentWord.word.f_lang : currentWord.word.n_lang}
        onClick={setIsNative}
      />
      <CardHolder />
    </ContentWrapper>
  );
};

export default observer(Content);
