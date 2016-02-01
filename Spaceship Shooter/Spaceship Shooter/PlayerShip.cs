using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Spaceship_shooter
{
    /// <summary>
    /// Class for the player's ship
    /// </summary>
    class PlayerShip
    {

        public Texture2D playerShip_texture; // holds the player's texture sprite
        public float player_x, player_y; // holds the players's x and y positions
        public float vel_x, vel_y; // holds the player's x and y velocities
        public float thrust = 0.05f; // holds the thrust of the player's ship
        public List<Missile> missile_list= new List<Missile>();
        public List<Asteroid> asteroid_list = new List<Asteroid>();
        public int missile_bounce_limit = 1;


        // constructor for PlayerShip class
        // takes a 2D texture sprite, and the initial x and y positions as arguments
        public PlayerShip(Texture2D texture, int x, int y)
        {
            playerShip_texture = texture;
            player_x = x;
            player_y = y;

            // sets initial velocities to 0
            vel_x = 0;
            vel_y = 0;
        }

        // member function to update the player's position
        public void Update(float playerMouse_angle)
        {
            // if W is pressed, increase player's velocity towards the mouse
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                vel_x += thrust * (float)System.Math.Cos(playerMouse_angle);
                vel_y += thrust * (float)System.Math.Sin(playerMouse_angle);
            }

            // updates player position based on velocity
            // may be better to use some kind of game time?
            player_x += vel_x;
            player_y += vel_y;

            // velocity decay terms (velocity accelerates if more than 1!)
            vel_x *= 0.995f;
            vel_y *= 0.995f;
        }

        // member function to draw the player's ship
        // takes a SpriteBatch and angle between the player and the mouse as arguments
        public void Draw(SpriteBatch spriteBatch, float playerMouse_angle)
        {
            Vector2 player_position = new Vector2(player_x - (playerShip_texture.Width / 2), player_y - (playerShip_texture.Height / 2));
            Rectangle source_rectangle = new Rectangle(0, 0, playerShip_texture.Width, playerShip_texture.Height);
            Vector2 rotation_origin = new Vector2(playerShip_texture.Width / 2, playerShip_texture.Height / 2);
            spriteBatch.Draw(playerShip_texture, player_position, source_rectangle, Color.White, playerMouse_angle, rotation_origin, 0.25f, SpriteEffects.None, 1);
        }
    }
}
