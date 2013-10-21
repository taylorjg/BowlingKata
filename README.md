
## Description

This is my attempt at the following bowling kata:

* http://codingdojo.org/cgi-bin/wiki.pl?KataBowling

I extended the scope of the kata because I wanted to include a console app that could generate a scorecard showing details of each frame.

Most of the action is in the <code>Frame</code> class which uses a <code>FrameState</code> enum to decide how to handle a roll. I then simply pass each roll to each frame until a frame claims the roll as belonging to that frame. This gives earlier frames an opportunity to include the roll in their score e.g. for a spare or strike.

## Screenshot

![Screenshot](https://raw.github.com/taylorjg/BowlingKata/master/Images/Screenshot.png "Screenshot")
