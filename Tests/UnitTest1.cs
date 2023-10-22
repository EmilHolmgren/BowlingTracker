using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using BowlingTracker;
using System.Data;

namespace Tests;

public class UnitTest1
{
    //Game tests
    [Fact]
    public void GameInit()
    {
        var game = new Game();
        Assert.Equal(0,game.GetCurrentScore(1));
        Assert.Equal(1,game.GetCurrentFrameNum());
        Assert.Equal(1,game.GetCurrentRoll());
        Assert.False(game.DidGameEnd());
    }

    [Fact]
    public void SetNextRollOnlyAllowCorrectInput()
    {
        var game = new Game();
        Assert.Throws<ArgumentException>( () => game.SetNextRoll(11));
    }

    [Fact]
    public void SetNextRollShouldUpdateScoreFrameAndRoll()
    {
        var game = new Game();

        game.SetNextRoll(4);
        Assert.Equal(0, game.GetCurrentScore(1));
        Assert.Equal(2, game.GetCurrentRoll());
        Assert.Equal(1, game.GetCurrentFrameNum());

        game.SetNextRoll(3);
        Assert.Equal(7, game.GetCurrentScore(1));
        Assert.Equal(1, game.GetCurrentRoll());
        Assert.Equal(2, game.GetCurrentFrameNum());
    }

    [Fact]
    public void SetNextRollShouldHandleStrikes()
    {
        var game = new Game();
        
        //Strike
        game.SetNextRoll(10);
        Assert.Equal(0, game.GetCurrentScore(1));
        Assert.Equal(2, game.GetCurrentFrameNum());
        
        game.SetNextRoll(4);
        game.SetNextRoll(2);
        Assert.Equal(16, game.GetCurrentScore(1));
        Assert.Equal(22, game.GetCurrentScore(2));
        Assert.Equal(3, game.GetCurrentFrameNum());

        game.SetNextRoll(10);
        game.SetNextRoll(10);
        game.SetNextRoll(9);
        game.SetNextRoll(0);

        Assert.Equal(51, game.GetCurrentScore(3));
        Assert.Equal(70, game.GetCurrentScore(4));
    }

    [Fact]
    public void SetNextRollShouldHandleSpares()
    {
        var game = new Game();
        
        //Spare
        game.SetNextRoll(8);
        game.SetNextRoll(2);
        Assert.Equal(0, game.GetCurrentScore(1));
        
        game.SetNextRoll(7);
        Assert.Equal(17, game.GetCurrentScore(1));
        
        game.SetNextRoll(3);
        game.SetNextRoll(4);
        Assert.Equal(31, game.GetCurrentScore(2));
    }

    [Fact]
    public void SpareInSecondLastGame()
    {
        var game = new Game();

        for (int i=0; i<16; i++)
        {
            game.SetNextRoll(0);
        }
        game.SetNextRoll(2);
        game.SetNextRoll(8);

        game.SetNextRoll(10);
        game.SetNextRoll(10);
        game.SetNextRoll(10);

        Assert.Equal(20, game.GetCurrentScore(9));
        Assert.Equal(50, game.GetCurrentScore(10));
    }

    [Fact]
    public void SpareInLastFrame()
    {
        var game = new Game();
        for (int i=0; i<9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(2);
        game.SetNextRoll(8);
        game.SetNextRoll(3);
        Assert.Equal(265, game.GetCurrentScore(10));
        Assert.True(game.DidGameEnd());
    }

    [Fact]
    public void LastFrameOnlyTwoRolls()
    {
        var game = new Game();
        for (int i=0; i<9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(4);
        game.SetNextRoll(2);
        Assert.Equal(256, game.GetCurrentScore(10));
    }

    [Fact]
    public void PerfectGame()
    {
        var game = new Game();
        for (int i=0; i<12; i++)
        {
            game.SetNextRoll(10);
        }
        Assert.Equal(300, game.GetCurrentScore(10));
        Assert.True(game.DidGameEnd());
    }

    [Fact]
    public void AllZeroGame()
    {
        var game = new Game();
        for (int i=0; i<20; i++)
        {
            game.SetNextRoll(0);
        }
        Assert.Equal(0, game.GetCurrentScore(10));
        Assert.True(game.DidGameEnd());
    }

    [Fact]
    public void GameShouldNotHaveEnded()
    {
        //Only 9 frames played
        var game = new Game();
        for (int i=0; i<18; i++)
        {
            game.SetNextRoll(4);
        }
        Assert.False(game.DidGameEnd());
        
        //Spare in last frame
        game = new Game();
        for (int i=0; i<9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(2);
        game.SetNextRoll(8);
        Assert.False(game.DidGameEnd());
    }

    [Fact]
    public void GameShouldHaveEnded()
    {
        //Last frame only 2 rolls
        var game = new Game();    
        for (int i = 0; i < 9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(2);
        game.SetNextRoll(7);
        Assert.True(game.DidGameEnd());

        //Last frame spare
        game = new Game();
        for (int i=0; i<9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(3);
        game.SetNextRoll(7);
        game.SetNextRoll(10);
        Assert.True(game.DidGameEnd());

        //Last frame strike + zeros
        game = new Game();
        for (int i=0; i<9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(10);
        game.SetNextRoll(0);
        game.SetNextRoll(0);
        Assert.True(game.DidGameEnd());
    }

    //Frame tests
    [Fact]
    public void FrameInit()
    {
        var frame1 = new Frame(false);
        var roll1 = frame1.GetRoll(1);
        Assert.Equal(-1,roll1);
        var roll2 = frame1.GetRoll(2);
        Assert.Equal(-1,roll2);
        var roll3 = frame1.GetRoll(3);
        Assert.Equal(-1,roll3);
        var lastFrame = frame1.IsLastFrame();
        Assert.False(lastFrame);
    }
    [Fact]
    public void SetRollShouldSetCorrectly()
    {
        var frame1 = new Frame(true);
        frame1.SetRoll(1,4);
        frame1.SetRoll(2,5);
        frame1.SetRoll(3,0);

        var roll1 = frame1.GetRoll(1);
        var roll2 = frame1.GetRoll(2);
        var roll3 = frame1.GetRoll(3);

        Assert.Equal(4,roll1);
        Assert.Equal(5,roll2);
        Assert.Equal(0,roll3);
    }

    [Fact]
    public void GetRollShouldOnlyAcceptValidInputs()
    {
        var frame1 = new Frame(false);
        
        Assert.Throws<ArgumentException>( () => frame1.GetRoll(4));

    }
}