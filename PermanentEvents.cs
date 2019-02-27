using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Reflection;
using System.IO;
using TShockAPI.Hooks;

namespace PermanentEvents
{
    [ApiVersion(2, 1)]
    public class PermanentEvents : TerrariaPlugin
    {
        #region Plugin Info
        public override string Name => "PermanentEvents";
        public override string Author => "tanpro260196/BMS";
        public override string Description => "Make server event permanent!";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        #endregion

        public ConfigFile Config = new ConfigFile();

        public PermanentEvents(Main game) : base(game)
        {
        }

        private void LoadConfig()
        {
            string path = Path.Combine(TShock.SavePath, "PermanentEvents.json");
            Config = ConfigFile.Read(path);
        }

        public override void Initialize()
        {
            LoadConfig();
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            GeneralHooks.ReloadEvent += reloadconfig;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void reloadconfig(ReloadEventArgs args)
        {
            string path = Path.Combine(TShock.SavePath, "PermanentEvents.json");
            Config = ConfigFile.Read(path);
            args.Player.SendSuccessMessage("[Permanent Events] Reload Complete!");
        }
        private void OnUpdate(EventArgs args)
        {
            if (Config.bloodmoon && !Main.dayTime && !Config.eclipse && !Main.bloodMoon)
            {
                TSPlayer.Server.SetBloodMoon(!Main.bloodMoon);
                return;
            }
            if (Config.eclipse && Main.hardMode && !Config.bloodmoon && !Main.eclipse)
            {
                TSPlayer.Server.SetEclipse(!Main.eclipse);
                return;
            }
            if ((!Main.raining) && (Config.rain))
            {
                Main.StartRain();
                return;
            }
            if ((!Terraria.GameContent.Events.Sandstorm.Happening) && (Config.sandstorm))
            {
                Terraria.GameContent.Events.Sandstorm.StartSandstorm();
                return;
            }
            return;
        }
    }
}


