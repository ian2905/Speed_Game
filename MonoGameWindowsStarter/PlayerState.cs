using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    public interface PlayerState
    {
        void Update(Player player, GameTime gameTime, List<Platform> platforms);
        void Draw(Player p, SpriteBatch spriteBatch);




    }
}
