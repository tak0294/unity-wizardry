using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room
{
	public Rect rect = new Rect();
}

public class Section
{
	public Section(float xMin, float yMin, float xMax, float yMax)
	{
		this.xMin = xMin;
		this.yMin = yMin;
		this.xMax = xMax;
		this.yMax = yMax;
	}
	
	public Section(Section src)
	{
		this.xMin = src.xMin;
		this.xMax = src.xMax;
		this.yMin = src.yMin;
		this.yMax = src.yMax;
	}
	
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
	public float width{
		get{return this.xMax - this.xMin;}
	}
	public float height
	{
		get{return this.yMax - this.yMin;}
	}
	
	public bool isSplittedVertical   = false;
	public bool isSplittedHorizontal = false;
	public Room room;
}

public class Pair
{
	public Section section0;
	public Section section1;
	public DungeonModel.PAIR_TYPE v_or_h;
}


public class DungeonModel : MonoBehaviour {
	
	public const int MAP_W = 100;
	public const int MAP_H = 40;
	public const int MINIMUM_ROOM_SIZE 			= 4;
	public const int MARGIN_BETWEEN_RECT_ROOM 	= 2;
	public const int MINIMUM_RECT_SIZE 			= MINIMUM_ROOM_SIZE + (MARGIN_BETWEEN_RECT_ROOM * 2);
	
	public enum PAIR_TYPE
	{
		VERTICAL,
		HORIZONTAL
	}
	
	public enum FLOOR_TYPE
	{
		NONE,
		FLOOR,
		WALL,
		VERTICAL_PATH,
		HORIZONTAL_PATH,
		OUT_OF_RANGE,
	}
	
	private System.Random random = new System.Random();
	
	private FLOOR_TYPE[,] map = new FLOOR_TYPE[MAP_H, MAP_W];
	
	private List<Section> rects    = new List<Section>();
	private BetterList<Room> rooms = new BetterList<Room>();
	private BetterList<Pair> pairs = new BetterList<Pair>();
	
	
	// Use this for initialization
	void Start ()
	{
		//Initialize();
	}
	
	/***************************************
	 * 初期化.
	 ***************************************/ 
	public void Initialize()
	{
		int ii,jj;
		for(ii=0;ii<MAP_H;ii++)
		{
			for(jj=0;jj<MAP_W;jj++)
			{
				map[ii,jj] = FLOOR_TYPE.NONE;
			}
		}
		
		// 区画分割.
		Section rootRect = AddRect(0,0,MAP_W-1,MAP_H-1);
		SplitRect(ref rootRect);
		MorePair();
		MakeRoom();
		DebugDisplay();
	}
	
	public FLOOR_TYPE[,] GetMap()
	{
		return map;
	}
	
	public FLOOR_TYPE GetMapAt(int x, int y)
	{
		return this.map[y,x];
	}
	
	public static int GetDrawPositionX(int x)
	{
		return -(MAP_W/2*32) + (x*32);
	}
	
	public static int GetDrawPositionY(int y)
	{
		return 360 - y*32;
	}
	
	/***************************************
	 * 左上の部屋.
	 ***************************************/
	public Room GetLeftTopRoom()
	{
		int minX = 65535;
		int minY = 65535;
		Room res = null;
		IEnumerator e = rooms.GetEnumerator();
		while(e.MoveNext())
		{
			Room room = (Room)e.Current;
			if((int)room.rect.xMin < minX &&
			   (int)room.rect.yMin < minY)
			{
				minX = (int)room.rect.xMin;
				minY = (int)room.rect.yMin;
				res = room;
			}
		}
		
		return res;
	}
	
	
	
	/***************************************
	 * ペアの追加.
	 ***************************************/ 
	Pair AddPair(PAIR_TYPE type, Section section0, Section section1)
	{
		Pair pair = new Pair();
		pair.section0 = section0;
		pair.section1 = section1;
		pair.v_or_h = type;
		pairs.Add(pair);
		
		return pair;
	}
	
	Section GetHorizontalNextSection(Section src)
	{
		IEnumerator e = rects.GetEnumerator();
		while(e.MoveNext())
		{
			Section section = (Section)e.Current;
			if(section != src && section.xMin == src.xMax)
				return section;
		}
		
		return null;
	}
	
	Section GetVerticalNextSection(Section src)
	{
		IEnumerator e = rects.GetEnumerator();
		while(e.MoveNext())
		{
			Section section = (Section)e.Current;
			if(section != src && section.yMin == src.yMax)
				return section;
		}
		
		return null;
	}
	
	void MorePair()
	{
		IEnumerator e = rects.GetEnumerator();
		while(e.MoveNext())
		{
			Section section = (Section)e.Current;
			Section horizontalNextSection = GetHorizontalNextSection(section);
			Section verticalNextSection   = GetVerticalNextSection(section);
			if(horizontalNextSection != null && random.Next(0, 64) == 0)
			{
				AddPair(PAIR_TYPE.HORIZONTAL, section, horizontalNextSection);
			}
			
			if(verticalNextSection != null && random.Next(0, 64) == 0)
			{
				AddPair(PAIR_TYPE.VERTICAL, section, verticalNextSection);
			}
		}
	}
	
	/***************************************
	 * 部屋の追加.
	 ***************************************/ 
	Room AddRoom(int MinX, int MinY, int MaxX, int MaxY)
	{
		Room room = new Room();
		room.rect.xMin = MinX;
		room.rect.xMax = MaxX;
		room.rect.yMin = MinY;
		room.rect.yMax = MaxY;
		
		rooms.Add(room);
		
		return room;
	}
	
	void MakeRoom()
	{
		IEnumerator e = rects.GetEnumerator();
		int x,y,h,w;
		while(e.MoveNext())
		{
			Section rect = (Section)e.Current;
			w = random.Next(MINIMUM_ROOM_SIZE, (int)rect.width - (MARGIN_BETWEEN_RECT_ROOM * 2) + 1);
			h = random.Next(MINIMUM_ROOM_SIZE, (int)rect.height - (MARGIN_BETWEEN_RECT_ROOM * 2) + 1);
			x = random.Next((int)rect.xMin + MARGIN_BETWEEN_RECT_ROOM, (int)rect.xMax - MARGIN_BETWEEN_RECT_ROOM - w + 1);
			y = random.Next((int)rect.yMin + MARGIN_BETWEEN_RECT_ROOM, (int)rect.yMax - MARGIN_BETWEEN_RECT_ROOM - h + 1);
			
			Room room = AddRoom(x,y,x+w,y+h);
			rect.room = room;
		}
	}
	
	/***************************************
	 * 区画の追加.
	 ***************************************/ 
	Section AddRect(int MinX, int MinY, int MaxX, int MaxY)
	{
		Section rect = new Section(MinX, MinY, MaxX, MaxY);
		rects.Add(rect);
		return rect;
	}
	Section AddRect(ref Section srcRect)
	{
		Section rect = new Section(srcRect);
		rects.Add(rect);
		return rect;
	}
	
	/***************************************
	 * 区画の分割.
	 ***************************************/ 
	void SplitRect(ref Section parentRect)
	{
		if(parentRect.height <= MINIMUM_RECT_SIZE * 2)
		{
			parentRect.isSplittedVertical = true;
		}
		
		if(parentRect.width <= MINIMUM_RECT_SIZE * 2)
		{
			parentRect.isSplittedHorizontal = true;
		}
		
		if(parentRect.isSplittedHorizontal && parentRect.isSplittedVertical)
		{
			return;
		}
		
		Section childRect = AddRect(ref parentRect);
		
		if(parentRect.isSplittedVertical == false)
		{
			int splitCoordY;
			splitCoordY = random.Next((int)parentRect.yMin + MINIMUM_RECT_SIZE,
									  (int)parentRect.yMax - MINIMUM_RECT_SIZE);
			parentRect.yMax = splitCoordY;
			childRect.yMin  = splitCoordY;
			parentRect.isSplittedVertical = true;
			childRect.isSplittedVertical  = true;
			AddPair(PAIR_TYPE.VERTICAL, parentRect, childRect);
			SplitRect(ref parentRect);
			SplitRect(ref childRect);
			
			return;
		} else {
			int splitCoordX;
			splitCoordX = random.Next((int)parentRect.xMin + MINIMUM_RECT_SIZE,
									  (int)parentRect.xMax - MINIMUM_RECT_SIZE);
			parentRect.xMax = splitCoordX;
			childRect.xMin  = splitCoordX;
			parentRect.isSplittedHorizontal = true;
			childRect.isSplittedHorizontal  = true;
			AddPair(PAIR_TYPE.HORIZONTAL, parentRect, childRect);
			SplitRect(ref parentRect);
			SplitRect(ref childRect);
			
			return;
		}
	}
	
	void HorizontalLine(int x0, int x1, int y, FLOOR_TYPE drawType = FLOOR_TYPE.HORIZONTAL_PATH)
	{
		int startX = Mathf.Min(x0, x1);
		int endX   = Mathf.Max(x0, x1);
		for(int jj=startX;jj<=endX;jj++)
		{
			map[y, jj] = drawType;
		}
	}
	
	void VerticalLine(int y0, int y1, int x, FLOOR_TYPE drawType = FLOOR_TYPE.VERTICAL_PATH)
	{
		int startY = Mathf.Min(y0, y1);
		int endY   = Mathf.Max(y0, y1);
		for(int ii=startY;ii<=endY;ii++)
		{
			map[ii, x] = drawType;
		}
	}
	
	FLOOR_TYPE GetFloorType(int x, int y)
	{
		if(x<0 || x > MAP_W-1)
			return FLOOR_TYPE.OUT_OF_RANGE;
		if(y<0 || y > MAP_H-1)
			return FLOOR_TYPE.OUT_OF_RANGE;
		
		return map[y,x];
	}
	
	/***************************************
	 * Debug表示.
	 ***************************************/ 
	void DebugDisplay()
	{
		int ii,jj;
		string output = "";
				
		/*************************************
		 * 部屋.
		 *************************************/ 
		IEnumerator e = rooms.GetEnumerator();
		while(e.MoveNext())
		{
			Room room = (Room)e.Current;
			for(ii=(int)room.rect.yMin;ii<(int)room.rect.yMax;ii++)
			{
				for(jj=(int)room.rect.xMin;jj<(int)room.rect.xMax;jj++)
				{
					map[ii,jj] = FLOOR_TYPE.FLOOR;
				}
				
				//部屋の輪郭.
				VerticalLine((int)room.rect.yMin, (int)room.rect.yMax-1, (int)room.rect.xMin, FLOOR_TYPE.WALL);
				VerticalLine((int)room.rect.yMin, (int)room.rect.yMax-1, (int)room.rect.xMax, FLOOR_TYPE.WALL);
				HorizontalLine((int)room.rect.xMin, (int)room.rect.xMax, (int)room.rect.yMin, FLOOR_TYPE.WALL);
				HorizontalLine((int)room.rect.xMin, (int)room.rect.xMax, (int)room.rect.yMax, FLOOR_TYPE.WALL);
			}
		}
		
		/************************************
		 * 通路.
		 ************************************/
		e = pairs.GetEnumerator();
		while(e.MoveNext())
		{
			Pair pair = (Pair)e.Current;
			int startX,endX,startY,endY;
			int startX2,endX2,startY2,endY2;
			if(pair.v_or_h == PAIR_TYPE.HORIZONTAL)
			{
				startX = (int)pair.section0.room.rect.xMax;
				endX   = (int)pair.section0.xMax;
				startY = random.Next((int)pair.section0.room.rect.yMin+1, (int)pair.section0.room.rect.yMax-1);
				
				HorizontalLine(startX, endX, startY);
				
				startX2 = (int)pair.section0.xMax;
				endX2   = (int)pair.section1.room.rect.xMin;
				startY2 = random.Next((int)pair.section1.room.rect.yMin+1, (int)pair.section1.room.rect.yMax-1);
				
				HorizontalLine(startX2, endX2, startY2);
				
				//通路同士をつなぐ.
				VerticalLine(startY, startY2, endX);
			}
			else
			{
				startX = random.Next((int)pair.section0.room.rect.xMin+1, (int)pair.section0.room.rect.xMax-1);
				startY = (int)pair.section0.room.rect.yMax;
				endY   = (int)pair.section0.yMax;
				
				VerticalLine(startY, endY, startX);
				
				startX2 = random.Next((int)pair.section1.room.rect.xMin+1,
								     (int)pair.section1.room.rect.xMax-1);
				startY2 = (int)pair.section0.yMax;
				endY2   = (int)pair.section1.room.rect.yMin;
				
				VerticalLine(startY2, endY2, startX2);
				
				//通路同士をつなぐ.
				HorizontalLine(startX, startX2, endY);
			}
		}
		
		for(ii=0;ii<MAP_H;ii++)
		{
			for(jj=0;jj<MAP_W;jj++)
			{
				if(map[ii,jj] == FLOOR_TYPE.HORIZONTAL_PATH ||
				   map[ii,jj] == FLOOR_TYPE.VERTICAL_PATH)
				{
					if(GetFloorType(jj-1, ii-1) == FLOOR_TYPE.NONE)
						map[ii-1, jj-1] = FLOOR_TYPE.WALL;
					if(GetFloorType(jj, ii-1) == FLOOR_TYPE.NONE)
						map[ii-1, jj] = FLOOR_TYPE.WALL;
					if(GetFloorType(jj+1, ii-1) == FLOOR_TYPE.NONE)
						map[ii-1, jj+1] = FLOOR_TYPE.WALL;
					
					if(GetFloorType(jj-1, ii+1) == FLOOR_TYPE.NONE)
						map[ii+1, jj-1] = FLOOR_TYPE.WALL;
					if(GetFloorType(jj, ii+1) == FLOOR_TYPE.NONE)
						map[ii+1, jj] = FLOOR_TYPE.WALL;
					if(GetFloorType(jj+1, ii+1) == FLOOR_TYPE.NONE)
						map[ii+1, jj+1] = FLOOR_TYPE.WALL;
					
					if(GetFloorType(jj-1, ii) == FLOOR_TYPE.NONE)
						map[ii, jj-1] = FLOOR_TYPE.WALL;
					if(GetFloorType(jj+1, ii) == FLOOR_TYPE.NONE)
						map[ii, jj+1] = FLOOR_TYPE.WALL;
					
				}
				
			}
		}
		
		for(ii=0;ii<MAP_H;ii++)
		{
			for(jj=0;jj<MAP_W;jj++)
			{
				if(map[ii,jj] == FLOOR_TYPE.WALL)
					output += "#";
				else if(map[ii,jj] == FLOOR_TYPE.FLOOR)
					output += ".";
				else if(map[ii,jj] == FLOOR_TYPE.HORIZONTAL_PATH)
					output += "-";
				else if(map[ii,jj] == FLOOR_TYPE.VERTICAL_PATH)
					output += "|";
				else
					output += ",";
			}
			
			output += "\n";
		}
		
		Debug.Log(output);
	}
}
