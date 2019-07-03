using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRCModLoader;
using VRCTools;
using EasyChat;
using UnityEngine;
using VRC.Core;

namespace Mod
{
    [VRCModInfo("EasyChat Informational", "1.0", "Bluscream")]
    public class Mod : VRCMod
    {
        // EasyChat.EasyChat easyChat;
        void OnApplicationStart() {
            Utils.Log("OnApplicationStart (Registering for commands)");
            if (!ModManager.Mods.Any(m => m.Name == "EasyChat")) {
                VRCUiPopupManagerUtils.ShowPopup("EasyChat not found", "The Mod EasyChat is required for this mod to work, please install it now or click ignore to continue without this mod",
                    "Install EasyChat", () => {
                        Application.OpenURL("https://link.to/easychat");
                        VRCUiPopupManagerUtils.GetVRCUiPopupManager().HideCurrentPopup();
                    }, "Unload and Continue", () => {
                        VRCUiPopupManagerUtils.GetVRCUiPopupManager().HideCurrentPopup();
                    });
                return;
            }
            // easyChat = new EasyChat.EasyChat();
            EasyChat.EasyChat.OnCommand += OnCommand;
        }
        void OnLevelWasLoaded(int level) {
            Utils.Log("OnLevelWasLoaded", level);
            // EasyChat.EasyChat.HandleMessage(new ChatMessage("test"));
        }
        void OnLevelWasInitialized(int level) {
            Utils.Log("OnLevelWasInitialized", level);
        }

        private void OnCommand(ChatCommand command)
        {
            ChatMessage answer = null;
            switch (command.Command) {
                case "userinfo":
                    if (APIUser.CurrentUser is null) {
                        answer = new ChatMessage("Not logged in!", sender: "User Information", sanitize: true);
                        answer.Color = Color.red;
                    } else answer = new ChatMessage(APIUser.CurrentUser.displayName, sender: "User Information", sanitize: true);
                    break;
                default:
                    return;
            }

            // easyChat.HandleMessage(answer);
        }
        void OnApplicationQuit() {
            Utils.Log("OnApplicationQuit");
        }

    }
}
