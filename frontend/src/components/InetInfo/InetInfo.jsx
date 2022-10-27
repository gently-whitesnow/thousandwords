import React from "react";
import { observer } from "mobx-react-lite";
import {
  AttentionReason,
  InetAttention,
  InetInfoWrapper,
  SaveBtn,
  WordsWrapper,
} from "./InetInfo.styles";

const InetInfo = (props) => {
  return (
    <InetInfoWrapper>
      <AttentionReason>Упс, интернет пропал !</AttentionReason>
      {props.words?.length > 0 ? (
        <>
          <WordsWrapper>
            <InetAttention>{`Мы не успели сохранить несколько слов: ${props.words?.length}`}</InetAttention>
            <InetAttention>
              Не перезагружайте страницу, нажмите сохранить как только появится
              интернет
            </InetAttention>
            {props.loading ? null : (
              <SaveBtn
                onClick={() => {
                  props.setLoading(true);
                  props.trySend();
                }}
              >
                &#x269B;
              </SaveBtn>
            )}
          </WordsWrapper>
        </>
      ) : null}
    </InetInfoWrapper>
  );
};

export default observer(InetInfo);
