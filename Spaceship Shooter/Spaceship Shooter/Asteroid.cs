using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Spaceship_shooter
{
    class Asteroid
    {
        // instance variables
        static Random rnd = new Random(); // must be static or random number stays constant and asteroids would be on top of each other
        public Texture2D asteroid_texture; // holds the asteroid's texture sprite
        public float asteroid_x, asteroid_y;  // asteroid x and y positions
        public float asteroid_vel_x, asteroid_vel_y; // asteroid x and y velocities
        public float asteroid_size; // asteroid scale factor
        public float asteroid_angle, asteroid_spin; // asteroid rotation characteristics


        // constructor
        public Asteroid(Texture2D texture)
        {
            asteroid_texture = texture;
            // assign random numbers to initial position, velocity, angle and spin speed
            asteroid_x = rnd.Next(50, 750);
            asteroid_y = rnd.Next(50, 430);
            asteroid_vel_x = rnd.Next(1, 3);
            asteroid_vel_y = rnd.Next(1, 3);
            asteroid_size = rnd.Next(1, 5);
            asteroid_angle = rnd.Next(1,2);
            asteroid_spin = rnd.Next(-4, 4);
        }


        // member function to update the player's position
        public void Update()
        {

            // updates asteroid position and angle
            // may be better to use some kind of game time?
            asteroid_x += asteroid_vel_x;
            asteroid_y += asteroid_vel_y;
            asteroid_angle += 0.01f*asteroid_spin;

            //bounce the asteroid!

            if (asteroid_x - (asteroid_texture.Width*asteroid_size/2) < 0 || asteroid_x + (asteroid_texture.Width*asteroid_size/2) > 800)
            {
                asteroid_vel_x = -asteroid_vel_x;
            }

            if (asteroid_y - (asteroid_texture.Height*asteroid_size/2) < 0 || asteroid_y + (asteroid_texture.Height*asteroid_size/2) > 480)
            {
                asteroid_vel_y = -asteroid_vel_y;
            }

            


        }
        


        public void Draw(SpriteBatch spriteBatch)
        {

            Vector2 asteroid_position = new Vector2(asteroid_x - (asteroid_texture.Width / 2), asteroid_y - (asteroid_texture.Height / 2));
            Rectangle asteroid_source_rectangle = new Rectangle(0, 0, asteroid_texture.Width, asteroid_texture.Height);
            Vector2 asteroid_rotation_origin = new Vector2(asteroid_texture.Width / 2, asteroid_texture.Height / 2);
            spriteBatch.Draw(asteroid_texture, asteroid_position, asteroid_source_rectangle, Color.White, asteroid_angle, asteroid_rotation_origin, asteroid_size, SpriteEffects.None, 1);

        }
    }

}
