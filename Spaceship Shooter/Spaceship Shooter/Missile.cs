using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship_shooter
{

    class Missile
    {

        public Texture2D missile_texture; // holds the missile's texture sprite
        public float missile_x, missile_y;  // missile x and y positions
        public float missile_vel_x, missile_vel_y;
        public float missile_mag_vel = 5;
        public float missile_angle;
        public int bounces_remaining;
        
        
        // constructor
        public Missile(Texture2D texture, PlayerShip player_ship, float playerMouse_angle)
        {
            missile_texture = texture;
            missile_x = player_ship.player_x + 15; // this is the player's position, centre of sprite
            missile_y = player_ship.player_y - 91;

            //missile has initial speed of magnitude 5, and fires in the mouse direction
            missile_vel_x = player_ship.vel_x + missile_mag_vel * (float)System.Math.Cos(playerMouse_angle);
            missile_vel_y = player_ship.vel_y + missile_mag_vel * (float)System.Math.Sin(playerMouse_angle);
            missile_angle = playerMouse_angle;

            //set the remaining bounces
            bounces_remaining = player_ship.missile_bounce_limit;
        }                
        
        // member function to update the player's position
        public void Update()
        {

            // updates missile position based on velocity of the missile         
            missile_x += missile_vel_x;
            missile_y += missile_vel_y;
                      


        }

        public void bounce()
        {
            //bounce the missile!

            if (missile_x < 30 || missile_x > 900)
            {
                missile_vel_x = -missile_vel_x;
                missile_angle = -missile_angle + (float)System.Math.PI;
                bounces_remaining--;
            }

            if (missile_y < 10 || missile_y > 500)
            {
                missile_vel_y = -missile_vel_y;
                missile_angle = -missile_angle;
                bounces_remaining--;
            }

        }

        // member function to draw the missile
        // takes a SpriteBatch and angle between the player and the mouse as arguments
        public void Draw(SpriteBatch spriteBatch, float missile_angle)
        {

            Vector2 missile_position = new Vector2(missile_x - (missile_texture.Width / 2), missile_y - (missile_texture.Height / 2));
            Rectangle missile_source_rectangle = new Rectangle(0, 0, missile_texture.Width, missile_texture.Height);
            Vector2 missile_rotation_origin = new Vector2(missile_texture.Width / 2, missile_texture.Height / 2);
            spriteBatch.Draw(missile_texture, missile_position, missile_source_rectangle, Color.White, missile_angle, missile_rotation_origin, 0.06f, SpriteEffects.None, 1);
            
        }
    }
}
