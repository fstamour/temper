\ find the leftmost run of 1s
: leftmost-run ( u1 - offset size )
  trim-0s
  swap
  trim-1s
  swap
  drop ;

: clear-run ( u1 offset size  - u2 )
  + 1s invert and ;

: spread (
  compilation: u -- ;
  interpretation: u1 -- u2
  )
  (
    u is the layout
    u1 is the input
    u2 is the output, where the bits
    of the input has been spread out
    according to the layout
    e.g. layout: %10101
          input: %0 1 1
         output: %00101
  )
  \ init result
  0 ]] literal swap [[
  0 locals| offset |
  begin
    dup \ used by "leftmost-run"
    dup \ used by "while"
  while
    \ get the length and offset of the
    \ leftmost run of of ones
    leftmost-run
    \ used by clear-run
    2dup
    \ compute the mask
    1s offset lshift
    \ compute the output's offset
    swap offset - swap
     \ keep a copy of the input
     postpone dup
     \ push and apply the mask
     ]] literal and [[
     \ push and apply the offset
     ]] literal lshift [[
     \ update the intermediate result
     \ and put back the input on top
    ]] rot or swap [[
    \ update the input's offset
    \ (adds the length of the mask)
    dup offset + to offset
    \ remove the run from the layout
    clear-run
  repeat
  \ drop the copy of the input
  postpone drop
  2drop ;

\ given a character, a layout, and a value
\ spread the bit of x accordingly to
\ to where the character c appears in the
\ layout.
\
\ This is roughly the run-time equivalent
\ of the compiler word "spread".
\
\ The layout could be seen as a set of
\ masks, where the character is used to
\ select which mask to use.
: >spread
  ( c-addr u c x - u )
  {: c x :}
  0 0 0 locals| ix res pos |
  over + 1-' 1-
  -do
    i c@ c =
    if
      ix 1-bit-mask x and ix rshift
      pos lshift
      res or to res
      ix 1+ to ix
    then
    pos 1+ to pos
  1 -loop
  res ;
