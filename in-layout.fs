\ buffer to store the layout string
\ that is currently being processed.
\ to help keep things simpler, we
\ store the layout stripped of
\ spaces
create layout #16 chars allot

\ initialize layout
: 0layout
  layout #16 chars erase ;

\ push layout addr and its size
: layout' layout #16 ;

\ store a layout, stripping spaces
: @layout
  0 -rot \ index into layout
  0 do
     dup
     i th-c@
     dup bl = if \ skip spaces
      drop \ drop char
    else
      ( index c-addr c )
      third layout th-c!
      1+'
    then
  loop
  2drop ;

\ print the stored layout
: .layout layout' type cr ;

: test-@layout
  s" oooo oord dddd rrrr"
  @layout
  .s
  layout' type ;