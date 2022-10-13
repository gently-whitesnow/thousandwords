import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { ContentWrapper } from "./Content.styles";
import CardHolder from "../CardHolder";
import LevelBar from "../LevelBar";
import LevelNumber from "../LevelNumber";
import { useStore } from "../../store";
import MindWord from "../MindWord";
import Auth from "../Auth";

const Content = () => {
  const { wordsStore, globalStore } = useStore();
  const { getWord, getOtherWords, currentWord, isNative, level, setIsNative } =
    wordsStore;
  const { auth } = globalStore;

  return (
    <ContentWrapper>
      {auth ? (
        <>
          <LevelNumber level={level} />
          <MindWord
            word={isNative ? currentWord.word.f_lang : currentWord.word.n_lang}
            onClick={setIsNative}
          />
          <CardHolder />
        </>
      ) : (
        <Auth />
      )}
    </ContentWrapper>
  );
};

export default observer(Content);
