﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(int hp)
    {
        slider.value = hp;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int maxHp)
    {
        slider.maxValue = maxHp;
        slider.value = maxHp;

        fill.color = gradient.Evaluate(1f);
    }
}
/*
<!DOCTYPE html>
<html lang="en-us">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>Unity WebGL Player | AIC21-Graphic</title>
    <link rel="shortcut icon" href="TemplateData/favicon.ico">
    <link rel="stylesheet" href="TemplateData/style.css">
  </head>
  <body>
    <div id="unity-container" class="unity-desktop">
      <canvas id="unity-canvas"></canvas>
      <div id="unity-loading-bar">
        <div id="unity-logo"></div>
        <div id="unity-progress-bar-empty">
          <div id="unity-progress-bar-full"></div>
        </div>
      </div>
      <div id="unity-footer">
        <div id="unity-webgl-logo"></div>
        <div id="unity-fullscreen-button"></div>
        <div id="unity-build-title">AIC21-Graphic</div>
      </div>
    </div>
    <script>
      var buildUrl = "Build";
      var loaderUrl = buildUrl + "/WebGLBuild.loader.js";
      var config = {
        dataUrl: buildUrl + "/WebGLBuild.data",
        frameworkUrl: buildUrl + "/WebGLBuild.framework.js",
        codeUrl: buildUrl + "/WebGLBuild.wasm",
        streamingAssetsUrl: "StreamingAssets",
        companyName: "DefaultCompany",
        productName: "AIC21-Graphic",
        productVersion: "1.0",
      };

      var container = document.querySelector("#unity-container");
      var canvas = document.querySelector("#unity-canvas");
      var loadingBar = document.querySelector("#unity-loading-bar");
      var progressBarFull = document.querySelector("#unity-progress-bar-full");
      var fullscreenButton = document.querySelector("#unity-fullscreen-button");

      if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
        container.className = "unity-mobile";
        config.devicePixelRatio = 1;
      } else {
        canvas.style.width = "1152px";
        canvas.style.height = "648px";
      }
      loadingBar.style.display = "block";

      var script = document.createElement("script");
      script.src = loaderUrl;
      script.onload = () => {
        createUnityInstance(canvas, config, (progress) => {
          progressBarFull.style.width = 100 * progress + "%";
        }).then((unityInstance) => {
          loadingBar.style.display = "none";
          fullscreenButton.onclick = () => {
            unityInstance.SetFullscreen(1);
          };
          fetch('test1.json')
                  .then(response => response.text())
                  .then(text => unityInstance.SendMessage('GameLogReader', 'MakeLog', text));
        }).catch((message) => {
          alert(message);
        }).catch((message) => {
          alert(message);
        });
      };
      document.body.appendChild(script);
    </script>
  </body>
</html>
*/