## 概要

VLiveKitの一部として開発している、  
ライブステージ構築用のアセットおよびツールをまとめたパッケージです。

Unity上でステージのレイアウトや検証を行うための  
基本的な要素を一通り含んでいます。

現在は構成の整理および調整を行っている段階です。

---

## 内容

### ステージ構築用アセット

以下のような基本要素を含みます：

- バミリ用デカール
- マイクモデル
- トラス
- アンプ
- 幕（バックドロップ）

ライブステージのレイアウト検討や、仮組み・検証用途を想定しています。

---

### トラス配置ツール（試作）

トラスを効率的に配置するためのビルダーツールを同梱しています。

現在は試験的な実装であり、実際の運用を通して調整・改善を行っています。

---

## 開発状況

本パッケージは現在整理中であり、構成や機能は今後変更される可能性があります。

---

## インストール

### manifest.json に追加

`Packages/manifest.json` の `dependencies` に以下を追加してください。

```json
{
  "dependencies": {
    "com.toshi.vlivekit.stagebuilder": "https://github.com/toshi-kundesu/VLiveKit_StageBuilder.git?path=/Assets/toshi.VLiveKit/StageBuilder#main"
  }
}
