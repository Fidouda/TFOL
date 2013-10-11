﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Breakout
{
    class Brique
    {
        private Texture2D sprite;
        private Color color;
        private Vector2 position;
        private int hp;

        public Brique(Texture2D sprite, Color color, Vector2 position, int hp)
        {
            this.sprite = sprite;
            this.color = color;
            this.position = position;
            this.hp = hp;
        }

        public void Update()
        {
            // TODO: faire de quoi
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, color);
        }
    }
}
