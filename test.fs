include avr.fs

: test-nop,
  #10 0assembly
  nop,
  #assembly @ 1 = \ TODO assert that this is true
  .assembly
  free-assembly ;

test-nop,

: test-cpc,
  #10 0assembly
  10 5 cpc,
  .assembly
  free-assembly ;

test-cpc,

: find-char-from-end ( c-addr u1 c - u2 )
  swap 0 swap
  -do
    over i + c@ over =
    if
      2drop
      i
      unloop exit
    then
  1 -loop
  2drop -1
  ;


: test-find-char-from-end
  s" abcd" [char] c
  find-char-from-end assert( 2 = )

  s" abcd" [char] f
  find-char-from-end assert( -1 = )
  ;
test-find-char-from-end


\ : test-1st-bit
\   cr
\   0 0 1st-bit .
\   #2 1 1st-bit .
\   #8 1 1st-bit .
\   %10111 0 1st-bit .
\   -1 0 1st-bit . ;


(
%10110 trim-0s .s
%111 trim-0s .s
)

(
%10110 trim-1s .s 2drop
%111 trim-1s .s
)

(
%1000111100 #2 #4
clear-run .ss
)

(
%1000111100 leftmost-run
.s
\ 2 4
)

(
: frob
  [ %1000111100 spread ] ;
see frob

$ffff frob cr .ss cr drop
#42 frob cr .ss cr drop
#43 frob cr .ss cr drop
#21 frob cr .ss cr drop
)





\ ==========================

\ we want to have some tests that write
\ files, so we compute a directory path
\ relative to the source files

\ the size of the path
variable /test-dir
\ the path
create test-dir

here
sourcefilename
ndup char / find-char-from-end
\ TODO this won't work if we put source
\ in a subdirectory of the current working
\ directory (I think)
dup -1 = [if]
  \ no slash found, so it's just a filename
  2drop
  unused
  get-dir
  allot
[else]
  dup allot
  nswap cmove
[then]


\ we append the name of the directory
here
s" /test-outputs/" dup allot
nswap cmove

\ we save the length of the path
here test-dir - /test-dir !

: test-dir' test-dir /test-dir @ ;

: test-dir/
  { dest c-addr u }
  test-dir' dest swap cmove
  c-addr dest /test-dir @ + u cmove
  dest /test-dir @ u + ;

test-dir'
%111110000 \ permission flags
mkdir-parents
\ skip "already exists" error
dup -529 <> [if] throw [else] drop [then]

pad s" output.bin" test-dir/
write-assembly

\ ==========================
