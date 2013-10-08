
## Description

This is my attempt at the following bowling kata:

* http://codingdojo.org/cgi-bin/wiki.pl?KataBowling

## Future Plans

* Given a sequence of rolls (params array of ints), return a string e.g.
    * 21 5's => "5/5/5/5/5/5/5/5/5/5/5"
    * 12 10's => "XXXXXXXXXXXX"
    * 9 then 0 times 10 => "9-9-9-9-9-9-9-9-9-9-"
    * etc.

* Experiment with feeding the rolls to the bowling calculator as an <code>IObservable&lt;int&gt;</code> (Reactive Extensions)

* Write a console program where the user can enter the rolls (as ints) and the bowling calculator will display the total score along with frame-by-frame details

