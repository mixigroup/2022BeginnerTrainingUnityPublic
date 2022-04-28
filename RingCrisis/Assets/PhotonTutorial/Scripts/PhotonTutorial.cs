using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace PhotonTutorial
{
    /// <summary>
    /// Photonを使った通信のチュートリアル
    /// 追いやすさのため、あえてすべての処理をこのクラスに押し込んでいます
    /// </summary>
    [DisallowMultipleComponent]
    public class PhotonTutorial : MonoBehaviourPunCallbacks
    {
        /// <summary>ログテキスト1行分のオブジェクトのプレハブ</summary>
        [SerializeField]
        private TMP_Text _logLinePrefab = null;

        /// <summary>ログテキストのルート</summary>
        [SerializeField]
        private Transform _logLineRoot = null;

        /// <summary>ニックネーム入力フィールド</summary>
        [SerializeField]
        private TMP_InputField _nickNameInputField = null;

        /// <summary>ルーム名入力フィールド</summary>
        [SerializeField]
        private TMP_InputField _roomNameInputField = null;

        /// <summary>メッセージ入力フィールド</summary>
        [SerializeField]
        private TMP_InputField _messageInputField = null;

        /// <summary>Connectボタン</summary>
        [SerializeField]
        private Button _connectButton = null;

        /// <summary>Disconnectボタン</summary>
        [SerializeField]
        private Button _disconnectButton = null;

        /// <summary>Create Roomボタン</summary>
        [SerializeField]
        private Button _createRoomButton = null;

        /// <summary>Join Roomボタン</summary>
        [SerializeField]
        private Button _joinRoomButton = null;

        /// <summary>Send RPCボタン</summary>
        [SerializeField]
        private Button _sendRpcButton = null;

        private List<TMP_Text> _logLines;

        // Photonネットワーク越しにRPCのやり取りをするために必要なコンポーネント
        private PhotonView _photonView;

        private void Awake()
        {
            // Assertがたくさん
            // ついサボってしまいがちだが、「インスペクタ上で参照が外れてしまった」といったC#外の問題は発見しづらいので
            // なるべく早期に分かるようにチェックしておくとよい（戒め）
            Assert.IsNotNull(_logLinePrefab);
            Assert.IsNotNull(_logLineRoot);
            Assert.IsNotNull(_nickNameInputField);
            Assert.IsNotNull(_roomNameInputField);
            Assert.IsNotNull(_messageInputField);
            Assert.IsNotNull(_connectButton);
            Assert.IsNotNull(_disconnectButton);
            Assert.IsNotNull(_createRoomButton);
            Assert.IsNotNull(_joinRoomButton);
            Assert.IsNotNull(_sendRpcButton);

            // 各種ボタンをクリックしたときのイベントを登録
            _connectButton.onClick.AddListener(Connect);
            _disconnectButton.onClick.AddListener(Disconnect);
            _createRoomButton.onClick.AddListener(CreateRoom);
            _joinRoomButton.onClick.AddListener(JoinRoom);
            _sendRpcButton.onClick.AddListener(SendSampleRpc);

            _logLines = new List<TMP_Text>();

            // この GameObject にアタッチされているPhotonViewコンポーネントを取得
            // もちろんボタンと同じように [SerializeField] で参照を持っても良い
            _photonView = GetComponent<PhotonView>();
        }


        // =======================================
        // ボタンクリック時のイベント実装
        // =======================================

        private void Connect()
        {
            // Photonサーバに接続
            if (!PhotonNetwork.ConnectUsingSettings())
            {
                AppendLog("ConnectUsingSettings error");
            }
        }

        private void Disconnect()
        {
            // Photonサーバから切断
            PhotonNetwork.Disconnect();
        }

        private void CreateRoom()
        {
            // ニックネームを設定
            PhotonNetwork.NickName = _nickNameInputField.text;

            // 入力されているルーム名でルーム作成
            if (!PhotonNetwork.CreateRoom(_roomNameInputField.text))
            {
                AppendLog("CreateRoom error");
            }
        }

        private void JoinRoom()
        {
            // ニックネームを設定
            PhotonNetwork.NickName = _nickNameInputField.text;

            // 入力されているルーム名でルーム入室
            if (!PhotonNetwork.JoinRoom(_roomNameInputField.text))
            {
                AppendLog("JoinRoom error");
            }
        }

        private void SendSampleRpc()
        {
            var message = _messageInputField.text;

            // このPhotonViewと同一のView IDを持つPhotonViewへRPCを送信
            // 与えるパラメータは [PunRPC] をつけたRPCメソッドの引数と合わせる
            _photonView.RPC("SampleRpc", RpcTarget.AllViaServer, PhotonNetwork.NickName, message);
        }

        // RPCメソッドの実装
        // PhotonView.RPCを使ってPhotonサーバ経由で各クライアントの同一メソッドを呼び出すことができる
        // 一応、普通のメソッドでもあるので普通に呼び出すこともできる（もちろんこの場合はPhotonは関係ない）
        [PunRPC]
        private void SampleRpc(string sender, string message)
        {
            AppendLog($"{sender}> {message}");
        }


        // =======================================
        // MonoBehaviourPunCallbacks
        // =======================================

        // Photonのマスターサーバに接続できたときに呼ばれる
        // これ以降、ルームの作成・入室ができる
        // ルームの一覧を取得するには、ここからさらにロビーに入る必要がある
        public override void OnConnectedToMaster()
        {
            AppendLog("OnConnectedToMaster");
            PhotonNetwork.JoinLobby();
        }

        // マスターサーバのロビーに入った
        // これ以降、ルーム一覧の取得ができる
        public override void OnJoinedLobby()
        {
            AppendLog("OnJoinedLobby");
        }

        // ロビー内のルーム一覧が更新された
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (var roomInfo in roomList)
            {
                AppendLog($"{roomInfo.Name} (removed={roomInfo.RemovedFromList})");
            }
        }

        // ルーム作成が正常に完了したときに呼ばれる
        public override void OnCreatedRoom()
        {
            AppendLog("OnCreatedRoom");
        }

        // ルーム入室が正常に完了したときに呼ばれる
        public override void OnJoinedRoom()
        {
            AppendLog("OnJoinedRoom");
        }

        // ルーム作成に失敗したときに呼ばれる
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            AppendLog($"OnCreateRoomFailed: {message} ({returnCode})");
        }

        // ルーム入室に失敗したときに呼ばれる
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            AppendLog($"OnJoinRoomFailed: {message} ({returnCode})");
        }

        // サーバから切断されたときに呼ばれる
        public override void OnDisconnected(DisconnectCause cause)
        {
            AppendLog($"OnDisconnected: {cause}");
        }


        // =======================================
        // その他
        // =======================================

        private void AppendLog(string message)
        {
            var logLine = Instantiate(_logLinePrefab, _logLineRoot, false);
            logLine.text = message;
            _logLines.Add(logLine);

            if (_logLines.Count == 10)
            {
                Destroy(_logLines[0].gameObject);
                _logLines.RemoveAt(0);
            }
        }

        // オブジェクトが破棄されるときに呼ばれる
        private void OnDestroy()
        {
            if (PhotonNetwork.IsConnected)
            {
                Disconnect();
            }
        }
    }
}
