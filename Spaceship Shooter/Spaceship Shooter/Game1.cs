using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace Spaceship_shooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // create instances of PlayerShip, Missile and Asteroid classes
        private PlayerShip player_ship;
        private Missile missile;
        private Asteroid asteroid;
             
        // images
        private Texture2D background;
        private Texture2D mouseSprite;
        

        // coordinates of mouse
        private float mouse_x, mouse_y;

        // mouse state
        MouseState previous_mouse_state;

        // angle from player to mouse
        private float playerMouse_angle;

        // if start of game (needed for asteroid initialisation)
        private bool start = true;

        private int asteroid_number = 3;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //store the current state of the mouse
            previous_mouse_state = Mouse.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("background");
            mouseSprite = Content.Load<Texture2D>("mouseSprite");

            // create and load the player's ship with initial position x=400, y=240
            player_ship = new PlayerShip(Content.Load<Texture2D>("player_ship2"), 400, 240);

            // create and load our asteroid[s]
            //asteroid = new Asteroid(Content.Load<Texture2D>("asteroid_sprite1"));

        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // if ESC is pressed, exit game
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            
            // if at start of game, append our asteroids to list
            if (start == true)
            {
                for (int i = 0; i < asteroid_number; i++)
                {
                    asteroid = new Asteroid(Content.Load<Texture2D>("asteroid_sprite1"));
                    System.Console.Write(asteroid.asteroid_x);
                    System.Console.Write("\n");
                    player_ship.asteroid_list.Add(asteroid);
                }
                start = false;
                System.Console.Write("Asteroid Count: ");
                System.Console.Write(player_ship.asteroid_list.Count);
                System.Console.Write("\n");
            }


            // get mouse coordinates and calculate angle between player and mouse
            MouseState mouse_state = Mouse.GetState();
            //MouseState old_mouse_state;
            mouse_x = mouse_state.X;
            mouse_y = mouse_state.Y;
            playerMouse_angle = (float)System.Math.Atan2((mouse_x - player_ship.player_x + (player_ship.playerShip_texture.Width / 2)), -(mouse_y - player_ship.player_y + (player_ship.playerShip_texture.Height / 2))) - (float)System.Math.PI / 2;

            //if left mouse button is pressed, construct a new missile
                      

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && previous_mouse_state.LeftButton == ButtonState.Released)
            {
                missile = new Missile(Content.Load<Texture2D>("missileSprite"), player_ship, playerMouse_angle);
                player_ship.missile_list.Add(missile); //stored in the object player_ship, that  way each ship stores info about its missiles.
                               
                /*
                System.Console.WriteLine(missile_list);
                System.Console.WriteLine(missile_list.Count);
                There is no console; can't really see a use for this
                */
            }

            for (int i = 0; i < player_ship.missile_list.Count; i++)
            {

                player_ship.missile_list[i].Update();

                //if the missile is off the screen, remove it
                if(player_ship.missile_list[i].missile_x>1100|| player_ship.missile_list[i].missile_x < 0|| player_ship.missile_list[i].missile_y > 600|| player_ship.missile_list[i].missile_y < 0)
                {
                    if (player_ship.missile_list[i].bounces_remaining > 0) player_ship.missile_list[i].bounce(); //bounce the missile

                    else player_ship.missile_list.RemoveAt(i); //if no bounces left, remove it
                }
            }

            // save the current mouse state for the next frame
            previous_mouse_state = Mouse.GetState();

            // update player's position
            player_ship.Update(playerMouse_angle);

            // update asteroid's position
            for (int i = 0; i < player_ship.asteroid_list.Count; i++)
            {
                //for (int j = 0; j < player_ship.missile_list.Count; j++)

                    
               // {

                    //if (player_ship.asteroid_list[i].asteroid_x  < player_ship.missile_list[j].missile_x && player_ship.asteroid_list[i].asteroid_x > player_ship.missile_list[j].missile_x)
                    //{
                        //player_ship.asteroid_list.RemoveAt(i);
                        //player_ship.missile_list.RemoveAt(j);
                    //}
                    //if (player_ship.asteroid_list[i].asteroid_y < player_ship.missile_list[j].missile_y && player_ship.asteroid_list[i].asteroid_y > player_ship.missile_list[j].missile_y)
                    //{
                        //player_ship.asteroid_list.RemoveAt(i);
                        //player_ship.missile_list.RemoveAt(j);
                    //}
                player_ship.asteroid_list[i].Update();
                //player_ship.missile_list[j].Update();
                //}
                
            }




            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // start drawing
            spriteBatch.Begin();

            // draw background
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);

            // draw missiles
            for (int i = 0; i < player_ship.missile_list.Count; i++)
            {
                player_ship.missile_list[i].Draw(spriteBatch, player_ship.missile_list[i].missile_angle); //draw a missile if we've got one, but underneath the player ship
            }

            // draw player ship
            player_ship.Draw(spriteBatch, playerMouse_angle);

            // draw asteroids
            for (int i = 0; i < player_ship.asteroid_list.Count; i++)
            {
                player_ship.asteroid_list[i].Draw(spriteBatch);
            }



            // draw mouse last so it is on top
            spriteBatch.Draw(mouseSprite, new Vector2(mouse_x, mouse_y), Color.White); // draw mouse (better way?)
          
            spriteBatch.End();
            // end drawing

            base.Draw(gameTime);
        }
    }
}
