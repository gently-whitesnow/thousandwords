import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { ContentWrapper } from "./Content.styles";
import CardHolder from "../CardHolder";
import LevelNumber from "../LevelNumber";
import { useStore } from "../../store";
import MindWord from "../MindWord";
import Auth from "../Auth";
import WinContent from "../WinContent";

const Content = () => {
  const { wordsStore, globalStore } = useStore();
  const { currentWord, isNative, level, setIsNative } = wordsStore;
  const { auth, authHandler } = globalStore;

  useEffect(() => {
    authHandler();
  }, [authHandler]);

  return (
    <ContentWrapper>
      {auth ? (
        <>
          <LevelNumber level={level} />
          <MindWord
            word={
              isNative ? currentWord?.word.f_lang : currentWord?.word.n_lang
            }
            onClick={setIsNative}
          />
          <CardHolder />
        </>
      ) : level !== 1000 ? (
        <Auth />
      ) : (
        <WinContent />
      )}
    </ContentWrapper>
  );
};

export default observer(Content);
