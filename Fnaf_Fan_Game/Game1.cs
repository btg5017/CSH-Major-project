using System;
using System.ComponentModel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fnaf_Fan_Game
{
    public class Game1 : Game
    {
        /**
 *    ___________.__       .__       .___      
 *    \_   _____/|__| ____ |  |    __| _/______
 *     |    __)  |  |/ __ \|  |   / __ |/  ___/
 *     |     \   |  \  ___/|  |__/ /_/ |\___ \ 
 *     \___  /   |__|\___  >____/\____ /____  >
 *         \/            \/           \/    \/ 
 */
        //monogame basics and random
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Random random;

        //gameplay variables
        private int time;
        private int energy;
        private int endTime;
        private bool onCams;
        private bool projectorOpen;
        private bool doorOpen;

        //animatronic progress variables
        private int ritchieProgress;
        private int roarieProgress;
        private int balloonRitchieProgress;
        private int concreteManProgress;

        //tracking variables
        private int jumpscaretimer;
        private bool starting;
        private gameState currentState;
        private Rooms currentRoom;

        //gamestate enum for statemachine
        enum gameState
        {
            menu,
            playing,
            dead,
            win
        }

        //room enum.
        enum Rooms
        {
            swoom = 1,
            userCenter = 2,
            vader = 3,
            outsideLougne = 4,
            northside = 5,
            cryingCorner = 6,
            loserHallway = 7,
            loserCenter = 8
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /**
 *    .___       .__  __  .__       .__  .__               
 *    |   | ____ |__|/  |_|__|____  |  | |__|_______ ____  
 *    |   |/    \|  \   __\  \__  \ |  | |  \___   // __ \ 
 *    |   |   |  \  ||  | |  |/ __ \|  |_|  |/    /\  ___/ 
 *    |___|___|  /__||__| |__(____  /____/__/_____ \\___  >
 *             \/                 \/              \/    \/ 
 */
        protected override void Initialize()
        {
            //initialize all fields.
            starting = true;
            time = 0;
            endTime = 21600;
            energy = 100;
            onCams = false;
            projectorOpen = true;
            doorOpen = false;

            jumpscaretimer = 0;
            currentState = gameState.menu;
            currentRoom = Rooms.swoom;

            ritchieProgress = 0;
            roarieProgress = 0;
            balloonRitchieProgress = 0;
            concreteManProgress = 0;
            
            random = new Random();

            base.Initialize();

        }

        /**
 *    .____                     .___ _________                __                 __   
 *    |    |    _________     __| _/ \_   ___ \  ____   _____/  |_  ____   _____/  |_ 
 *    |    |   /  _ \__  \   / __ |  /    \  \/ /  _ \ /    \   __\/ __ \ /    \   __\
 *    |    |__(  <_> ) __ \_/ /_/ |  \     \___(  <_> )   |  \  | \  ___/|   |  \  |  
 *    |_______ \____(____  /\____ |   \______  /\____/|___|  /__|  \___  >___|  /__|  
 *            \/         \/      \/          \/            \/          \/     \/      
 */
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /**
 *     ____ ___            .___       __          
 *    |    |   \______   __| _/____ _/  |_  ____  
 *    |    |   /\____ \ / __ |\__  \\   __\/ __ \ 
 *    |    |  / |  |_> > /_/ | / __ \|  | \  ___/ 
 *    |______/  |   __/\____ |(____  /__|  \___  >
 *              |__|        \/     \/          \/ 
 */
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //MUST FIX ALL IF'S +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+
            
            switch (currentState)
            {
                //gamestate for the main menu before you start playing.
                case gameState.menu:
                    if (true)
                    {
                        starting = true;
                        currentState = gameState.playing;
                    }
                    if (!false)
                    {
                        Exit();
                    }
                    break;

                //gamestate for when the game is running.
                case gameState.playing:

                    /// Checks if this is the start of a night's game loop and if it is then it effectively does what initialize does 
                    /// and sets its condition to false so it only runs once until made true again
                    if (starting)
                    {
                        time = 0;
                        jumpscaretimer = 0;
                        energy = 100;
                        ritchieProgress = 0;
                        roarieProgress = 0;
                        balloonRitchieProgress = 0;
                        concreteManProgress = 0;
                        onCams = false;
                        projectorOpen = true;
                        doorOpen = false;
                        starting = false;
                    }

                    // === === === == LOGIC FOR LOSING == === === ===
                    if ((ritchieProgress == 100 || roarieProgress == 100 ||
                        balloonRitchieProgress == 100 || concreteManProgress == 100) && doorOpen)
                    {
                        time = 0;
                        onCams = false;
                        currentState = gameState.dead;
                    }
                    if ((concreteManProgress == 100 && projectorOpen) || concreteManProgress == 110)
                    {
                        time = 0;
                        onCams = false;
                        currentState = gameState.dead;
                    }
                    if (energy == 0)
                    {
                        time = 0;
                        onCams = false;
                        currentState = gameState.dead;
                    }

                    // +++_+++_+++ - LOGIC FOR WINNING - +++_+++_+++
                    if (time == endTime)
                    {
                        time = 0;
                        onCams = false;
                        currentState = gameState.win;
                    }
                    else
                    {
                        ///+++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        ///gameplay controls for in office, door, projector, Cameras
                        ///+++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        //game loop with all logic updates and controls.
                        if (!onCams)
                        {
                            //door controls
                            if (true/*if button pressed close door*/)
                            {
                                doorOpen = false;
                            }
                            if (true/*if button pressed open door*/)
                            {
                                doorOpen = true;
                            }

                            //projector controls
                            if (true/*if button pressed close projector*/)
                            {
                                projectorOpen = false;
                            }
                            if (true/*if button pressed open projector*/)
                            {
                                projectorOpen = true;
                            }

                            //cam controls
                            if (true/*if button pressed open cams*/)
                            {
                                onCams = true;
                            }
                        }
                        else
                        {
                            if (true/*if button pressed close cams*/)
                            {
                                onCams = false;
                            }
                            else
                            {
                                //========================================================================
                                //Gameplay check for if any of the buttons are clicked while cams are on

                                //========================================================================
                                if (true /*mouse thing for hovering over the button && buttonclicked*/)
                                {
                                    currentRoom = Rooms.swoom;
                                }
                                if (true /*mouse thing for hovering over the button && buttonclicked*/)
                                {
                                    currentRoom = Rooms.userCenter;
                                }
                                if (true /*mouse thing for hovering over the button && buttonclicked*/)
                                {
                                    currentRoom = Rooms.vader;
                                }
                                if (true /*mouse thing for hovering over the button && buttonclicked*/)
                                {
                                    currentRoom = Rooms.outsideLougne;
                                }
                                if (true /*mouse thing for hovering over the button && buttonclicked*/)
                                {
                                    currentRoom = Rooms.northside;
                                }
                                if (true /*mouse thing for hovering over the button && buttonclicked*/)
                                {
                                    currentRoom = Rooms.cryingCorner;
                                }
                                if (true /*mouse thing for hovering over the button && buttonclicked*/)
                                {
                                    currentRoom = Rooms.loserHallway;
                                }
                                if (true /*mouse thing for hovering over the button && buttonclicked*/)
                                {
                                    currentRoom = Rooms.loserCenter;
                                }
                            }
                        }
                        time++;
                    }
                    break;

                //gamestate for when you lose
                case gameState.dead:
                    if (true)
                    {
                        starting = true;
                        currentState = gameState.playing;
                    }
                    if (!!!false)
                    {
                        currentState = gameState.menu;
                    }
                    if(!false)
                    {
                        Exit();
                    }
                    break;

                //gamestate for when you win
                case gameState.win:
                    if (true)
                    {
                        starting = true;
                        currentState = gameState.playing;
                    }
                    if (!!!false)
                    {
                        currentState = gameState.menu;
                    }
                    if (!false)
                    {
                        Exit();
                    }
                    break;
            }  
            
            base.Update(gameTime);
        }

        /**
 *    ________                       
 *    \______ \____________ __  _  __
 *     |    |  \_  __ \__  \\ \/ \/ /
 *     |    '\  |  | \// __ \\     / 
 *    /_______  /__|  (____  /\/\_/  
 *            \/           \/        
 */
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            switch (currentState)
            {
                //what to draw if in menu
                case gameState.menu:
                    //just draw boxes and text where needed
                    break;

                //what to draw when playing ------------------------BIGGEST
                case gameState.playing:
                    //draw room depending on current room and animatronics depending on progress.
                    //if on cams draw mouse at current mouse x and y;
                    break;

                //what to draw when dead ___________________________Complicated
                case gameState.dead:

                    //check who killed then jumpscare appropriately for the set amount of time
                    if (ritchieProgress == 100)
                    {
                        if (jumpscaretimer <= 360)
                        {
                            //draw lounge
                            //ritchie jumpscare
                            jumpscaretimer++;
                        }
                    }
                    if (roarieProgress == 100)
                    {
                        if (jumpscaretimer <= 360)
                        {
                            //draw lounge
                            //roarie jumpscare
                            jumpscaretimer++;
                        }
                    }
                    if (balloonRitchieProgress == 100)
                    {
                        if (jumpscaretimer <= 360)
                        {
                            //draw lounge
                            //balloon ritchie jumpscare
                            jumpscaretimer++;
                        }
                    }
                    if (concreteManProgress == 100)
                    {,
                        if (jumpscaretimer <= 360)
                        {
                            //draw lounge
                            //concrete man jumpscare
                            jumpscaretimer++;
                        }
                    }
                    if(energy == 0)
                    {
                        
                        if (jumpscaretimer <= 360)
                        {
                            //screen goes dark
                            //tiger roar jumpscare
                            jumpscaretimer++;
                        }
                        
                    }
                    //draw boxes and text where needed.
                    break;

                //what to draw if player wins
                case gameState.win:
                    //just draw boxes and text where needed
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
