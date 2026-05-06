# VLiveKit StageBuilder

ライブステージ構築用の asset と editor tool をまとめた Unity package です。

## Package

- Package name: `com.toshi.vlivekit.stagebuilder`
- Version: `0.0.6`
- Unity: 2022.3
- Repository: https://github.com/toshi-kundesu/VLiveKit_StageBuilder
- Package root: `Assets/toshi.VLiveKit/StageBuilder`

## 主な内容

- stage layout 検証用の基本 asset
- truss / panel / prop などの stage element
- 検証・仮組み向けの builder utility

## 依存・同梱 asset

- Cinemachine 2.9.7
- com.toshi.vlivekit.testassetscontainer

## インストール

Unity の `Packages/manifest.json` の `dependencies` に追加します。

```json
{
  "dependencies": {
    "com.toshi.vlivekit.stagebuilder": "https://github.com/toshi-kundesu/VLiveKit_StageBuilder.git?path=/Assets/toshi.VLiveKit/StageBuilder#main"
  }
}
```

VLiveKit sandbox では submodule として `Packages/VLiveKit_StageBuilder` に配置し、`file:` 参照で読み込んでいます。

## 注意

- 本番 asset 化する前のステージ検証・構成整理に使う package です。

## License

この package 独自のコードと asset は repository の `LICENSE` に従います。third-party asset を含む場合は、それぞれの license / README を確認してください。
