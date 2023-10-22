using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using BowlingTracker;
using System.Data;

namespace Tests;

public class UnitTest1
{
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
    public void OnlyAllowCorrectInput()
    {
        var game = new Game();
        Assert.Throws<ArgumentException>( () => game.SetNextRoll(11));
    }
    

    [Fact]
    public void SetNextRollShouldUpdateScoreFrameAndRoll()
    {
        var game = new Game();

        game.SetNextRoll(4);
        Assert.Equal(0,game.GetCurrentScore(1));
        Assert.Equal(2,game.GetCurrentRoll());
        Assert.Equal(1,game.GetCurrentFrameNum());

        game.SetNextRoll(3);
        Assert.Equal(7,game.GetCurrentScore(1));
        Assert.Equal(1,game.GetCurrentRoll());
        Assert.Equal(2,game.GetCurrentFrameNum());

        //Strike
        game.SetNextRoll(10);
        Assert.Equal(0,game.GetCurrentScore(2));
        Assert.Equal(3,game.GetCurrentFrameNum());
        
        game.SetNextRoll(0);
        game.SetNextRoll(2);
        Assert.Equal(19, game.GetCurrentScore(2));
        Assert.Equal(21,game.GetCurrentScore(3));
        Assert.Equal(4,game.GetCurrentFrameNum());

        //Spare
        game.SetNextRoll(8);
        game.SetNextRoll(2);
        Assert.Equal(0, game.GetCurrentScore(4));
        
        game.SetNextRoll(7);
        Assert.Equal(38, game.GetCurrentScore(4));
        game.SetNextRoll(1);
        Assert.Equal(46, game.GetCurrentScore(5));
    }

    [Fact]
    public void PerfectGame()
    {
        var game = new Game();
        for (int i=0; i<12; i++)
        {
            game.SetNextRoll(10);
        }
        Assert.Equal(300,game.GetCurrentScore(10));
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
        Assert.Equal(00,game.GetCurrentScore(10));
        Assert.True(game.DidGameEnd());
    }

    [Fact]
    public void LastFrameSpare()
    {
        var game = new Game();
        for (int i=0; i<9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(2);
        game.SetNextRoll(8);
        game.SetNextRoll(3);
        Assert.Equal(265,game.GetCurrentScore(10));
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
        Assert.Equal(256,game.GetCurrentScore(10));
        Assert.True(game.DidGameEnd());
    }

    [Fact]
    public void GameShouldNotHaveEnded()
    {
        var game = new Game();
        for (int i=0; i<9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(2);
        game.SetNextRoll(8);
        Assert.False(game.DidGameEnd());

        game = new Game();
        game.SetNextRoll(10);
        game.SetNextRoll(3);
        game.SetNextRoll(7);
        game.SetNextRoll(4);
        game.SetNextRoll(3);
        game.SetNextRoll(0);
        game.SetNextRoll(0);
        game.SetNextRoll(0);
        game.SetNextRoll(0);
        game.SetNextRoll(0);
        game.SetNextRoll(5);
        game.SetNextRoll(5);
        game.SetNextRoll(5);
        game.SetNextRoll(10);
        game.SetNextRoll(10);
        Assert.False(game.DidGameEnd());
    }

    [Fact]
    public void GameShouldHaveEnded()
    {
        var game = new Game();
        for (int i=0; i<9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(2);
        game.SetNextRoll(7);
        Assert.True(game.DidGameEnd());

        game = new Game();
        for (int i=0; i<9; i++)
        {
            game.SetNextRoll(10);
        }
        game.SetNextRoll(3);
        game.SetNextRoll(7);
        game.SetNextRoll(10);
        Assert.True(game.DidGameEnd());

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
    public void FrameMethods()
    {
        var frame1 = new Frame(false);
        frame1.SetRoll(1,4);
        frame1.SetRoll(2,5);
        frame1.SetRoll(3,0);
        var roll1 = frame1.GetRoll(1);
        Assert.Equal(4,roll1);
        var roll2 = frame1.GetRoll(2);
        Assert.Equal(5,roll2);
        var roll3 = frame1.GetRoll(3);
        Assert.Equal(0,roll3);
    }

    /*Need test
        - only allow a max knocked down pins of 10 per frame (30 in last frame).
    */
}