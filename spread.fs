\ find the leftmost run of 1s
: leftmost-run ( u1 - offset size )
  trim-0s
  swap
  trim-1s
  swap
  drop ;

(
%1000111100 leftmost-run
.s
\ 2 4
)

: clear-run ( u1 offset size  - u2 )
  + 1s invert and ;

(
%1000111100 #2 #4
clear-run .ss
)

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
    \ leftmon run of of ones
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


(
: frob 
  [ %1000111100 spread ] ; 
see frob

$ffff frob cr .ss cr drop
#42 frob cr .ss cr drop
#43 frob cr .ss cr drop
#21 frob cr .ss cr drop
)