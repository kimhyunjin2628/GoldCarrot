using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    //카메라
    public GameObject cameraPosition;

    //메인플레이어
    public GameObject main_player;
    player_script player_script;

    public bool move_camera = false;//카메라 이동중(상속불가)
    public bool moveOnly_camera = false;

    //카메라이동좌표
    public Vector3 t_destination = Vector3.zero;
    public Vector3 r_destination = Vector3.zero;
    //카메라 rotation좌표
    public float camera_rotation_x = 0.0f;
    public float camera_rotation_y = 0.0f;
   public  float camera_rotation_z = 0.0f;
    //카메라 rotation방향
    public bool camera_rotation_x_up = false;
    public bool camera_rotation_y_up = false;
    public bool camera_rotation_z_up = false;
    //카메라 회전방향
    public bool rotationClockWise_x = false;//시계방향
    public bool rotationAntiClockWise_x = false;//반시계방향
    public bool rotationClockWise_y = false;//시계방향
    public bool rotationAntiClockWise_y = false;//반시계방향
    public bool rotationClockWise_z = false;//시계방향
    public bool rotationAntiClockWise_z = false;//반시계방향
    //카메라회전값
    public  float rotationValue_x = 0;
    public float rotationValue_y = 0;
    public float rotationValue_z = 0;
    //카메라 이동속도
    public float position_speed = 0f;
    //카메라회전속도 -> 이동시간(이동거리)기준
    public float rotation_time = 3.0f;
    public float rotation_speed_z = 3.0f;
    public float rotation_speed_y = 3.0f;
    public float rotation_speed_x = 3.0f;

    //카메라 position이동방향
    public bool camera_x_plus = false;
    public bool camera_x_minus = false;
    public bool camera_y_plus = false;
    public bool camera_y_minus = false;
    public bool camera_z_plus = false;
    public bool camera_z_minus = false;
    //카메라 현재 지점
    public float camera_position_x = 0f;
    public float camera_position_y = 0f;
    public float camera_position_z = 0f;
    //카메라 position도착 지점
    Vector3 camera_position_destination = Vector3.zero;


    //카메라 potation 도착
    public bool camera_potation_x_arrive = false;
    public bool camera_potation_y_arrive = false;
    public bool camera_potation_z_arrive = false;
    //카메라 rotation 도착
    public bool camera_rotation_x_arrive = false;
    public bool camera_rotation_y_arrive = false;
    public bool camera_rotation_z_arrive = false;
    //카메라 도착
    public bool t_camera_arrive = false;//카메라 이동완료
    public bool r_camera_arrive = false;//카메라 회전완료

    //카메라 도착후 좌표고정
    Vector3 tCameraVector;//메인카메라 고정좌표position
    Vector3 rCameraVector;//메인카메라 고정좌표rotation
    Vector3 tCameraPVector;//카메라포지션 고정좌표position

    //플레이어상태
    public int playerStep = 0; //0.normal 1.move 2.noaction 3.shopping
    // Start is called before the first frame update
    void Start()
    {
        player_script = main_player.GetComponent<player_script>();

        //이동+회전
        /* t_destination = new Vector3(-6.8f, 10f, 86.3f);//도착포지션
         r_destination = new Vector3(-22f+360f, -65+360f, 18.344f);//도착로테이션
         MoveCamera(this.transform.localEulerAngles, r_destination,2.0f);//test
         player_script.next_step = player_script.STEP.NOACTION;*/

        //메인카메라이동
        /*r_destination = new Vector3(60f,358f,358f);
        t_camera_arrive = true;
        MoveOnlyCamera(this.transform.localPosition,new Vector3(0.03f, 20.0f, -1.4f));
        MoveCamera(this.transform.localEulerAngles, r_destination,2.0f);*/

    }
    // Update is called once per frame
    void Update()
    {
        if (move_camera == true)//카메라 이동,회전
        {
            if (Vector3.Distance(this.transform.position, t_destination) < 1.0f)
            {
                //좌표고정
               // this.transform.localPosition = tCameraPVector;

                t_camera_arrive = true;//이동완료
            }
            else if(t_camera_arrive == false)
                this.cameraPosition.transform.Translate((t_destination - this.transform.position).normalized * position_speed * Time.deltaTime);

            //회전
            //x축
            if ((rotationClockWise_x == true || rotationAntiClockWise_x == true) && camera_rotation_x_arrive == false)
            {
                rotationValue_x -= Time.deltaTime * rotation_speed_x;//rotation_time초까지 회전     
                if (rotationValue_x <= 0)//회전완료
                {
                    rotationClockWise_x = false;
                    rotationAntiClockWise_x = false;
                    camera_rotation_x_arrive = true;
                }
                else
                {
                    if (rotationClockWise_x == true)
                        camera_rotation_x += Time.deltaTime * rotation_speed_x;
                    else if (rotationAntiClockWise_x == true)
                        camera_rotation_x -= Time.deltaTime * rotation_speed_x;
                }
            }
            //y축
            if ((rotationClockWise_y == true || rotationAntiClockWise_y == true) && camera_rotation_y_arrive == false)
            {
                rotationValue_y -= Time.deltaTime * rotation_speed_y;//rotation_time초까지 회전
                if (rotationValue_y <= 0)//회전완료
                {
                    rotationClockWise_y = false;
                    rotationAntiClockWise_y = false;
                    camera_rotation_y_arrive = true;
                }
                else
                {
                    if (rotationClockWise_y == true)
                        camera_rotation_y += Time.deltaTime * rotation_speed_y;
                    else if (rotationAntiClockWise_y == true)
                        camera_rotation_y -= Time.deltaTime * rotation_speed_y;
                }
            }
            //z축
            if ((rotationClockWise_z == true || rotationAntiClockWise_z == true) && camera_rotation_z_arrive == false)
            {
                rotationValue_z -= Time.deltaTime * rotation_speed_z;//rotation_time초까지 회전
                if (rotationValue_z <= 0)//회전완료
                {
                    rotationClockWise_z = false;
                    rotationAntiClockWise_z = false;
                    camera_rotation_z_arrive = true;
                }
                else
                {
                    if (rotationClockWise_z == true)
                        camera_rotation_z += Time.deltaTime * rotation_speed_z;
                    else if (rotationAntiClockWise_z == true)
                        camera_rotation_z -= Time.deltaTime * rotation_speed_z;
                }
            }
            //Debug.Log("xV: " + rotationValue_x + " yV: " + rotationValue_y + " zV: " + rotationValue_z + "   " + camera_rotation_x +" "+camera_rotation_y +" "+camera_rotation_z);
            //회전
            this.transform.localEulerAngles = new Vector3(camera_rotation_x, camera_rotation_y, camera_rotation_z);
            //회전완료
            if (camera_rotation_x_arrive == true && camera_rotation_y_arrive == true && camera_rotation_z_arrive == true)
            {
                //좌표고정
               // this.transform.localEulerAngles = rCameraVector;

                //도착완료
                r_camera_arrive = true;
            }

            //이동종료
            if (r_camera_arrive == true && t_camera_arrive == true)
            {
                //플래그 초기화
                move_camera = false;//이동종료

                rotationClockWise_x = false;
                rotationClockWise_y = false;
                rotationClockWise_z = false;
                rotationAntiClockWise_x = false;
                rotationAntiClockWise_y = false;
                rotationAntiClockWise_z = false;

                camera_rotation_x_arrive = false;
                camera_rotation_y_arrive = false;
                camera_rotation_z_arrive = false;

                camera_potation_x_arrive = false;
                camera_potation_y_arrive = false;
                camera_potation_z_arrive = false;

                r_camera_arrive = false;
                t_camera_arrive = false;

                //플래그초기화 2
               /* t_destination = Vector3.zero;
                r_destination = Vector3.zero;

                camera_rotation_x = 0f;
                camera_rotation_y = 0f;
                camera_rotation_z = 0f;

                rotationValue_x = 0;
                rotationValue_y = 0;
                rotationValue_z = 0;
                position_speed = 0;
                rotation_speed_x = 3;
                rotation_speed_y = 3;
                rotation_speed_z = 3;
                
                camera_position_x = 0;
                camera_position_y = 0;
                camera_position_z = 0;*/

                //플레이어상태
                switch (playerStep)
                {
                    case 0:
                        player_script.next_step = player_script.STEP.NORMAL;
                        break;
                    case 1:
                        player_script.next_step = player_script.STEP.MOVE;
                        break;
                    case 2:
                        player_script.next_step = player_script.STEP.NOACTION;
                        break;
                    case 3:
                        player_script.next_step = player_script.STEP.SHOPPING;
                        break;
                    case 4:
                        player_script.next_step = player_script.STEP.AUCTION;
                        break;
                }
               // player_script.next_step = player_script.STEP.NORMAL;
            }
        }//end of Move_camera
        //메인카메라 이동
        if (moveOnly_camera == true)
        {
            //x축
            if (camera_x_plus == true && this.transform.localPosition.x < camera_position_destination.x && camera_potation_x_arrive == false)
                camera_position_x += 10.0f * Time.deltaTime;
            else if (camera_x_minus == true && this.transform.localPosition.x > camera_position_destination.x && camera_potation_x_arrive == false)
                camera_position_x -= 10.0f * Time.deltaTime;
            else
            {
                camera_potation_x_arrive = true;//도착완료
                camera_x_plus = false;
                camera_x_minus = false;
            }

            //y축
            if (camera_y_plus == true && this.transform.localPosition.y < camera_position_destination.y && camera_potation_y_arrive == false)
                camera_position_y += 10.0f * Time.deltaTime;
            else if (camera_y_minus == true && this.transform.localPosition.y > camera_position_destination.y && camera_potation_y_arrive == false)
                camera_position_y -= 10.0f * Time.deltaTime;
            else
            {
                //Debug.Log("도착완료Y" + this.transform.localPosition.y + " . " + camera_position_destination.y );
                camera_potation_y_arrive = true;//도착완료
                camera_y_plus = false;
                camera_y_minus = false;
            }

            //z축
            if (camera_z_plus == true && this.transform.localPosition.z < camera_position_destination.z && camera_potation_z_arrive == false)
                camera_position_z += 10.0f * Time.deltaTime;
            else if (camera_z_minus == true && this.transform.localPosition.z > camera_position_destination.z && camera_potation_z_arrive == false)
                camera_position_z -= 10.0f * Time.deltaTime;
            else
            {
                camera_potation_z_arrive = true;//도착완료
                camera_z_plus = false;
                camera_z_minus = false;
            }

            //이동
            this.transform.localPosition = new Vector3(camera_position_x, camera_position_y, camera_position_z);
            //도착완료
            if (camera_potation_x_arrive == true && camera_potation_y_arrive == true && camera_potation_z_arrive == true)
            {
                //좌표고정
                //this.transform.localPosition = tCameraVector;

                //플래그초기화
                moveOnly_camera = false;

                camera_rotation_x_arrive = false;
                camera_rotation_y_arrive = false;
                camera_rotation_z_arrive = false;

                camera_potation_x_arrive = false;
                camera_potation_y_arrive = false;
                camera_potation_z_arrive = false;

                r_camera_arrive = false;
                t_camera_arrive = false;

                // player_script.next_step = player_script.STEP.NORMAL;
                Debug.Log("이동완료");
            }

        }//end of MoveOnly_camera


    }//end of Update

    /*이동함수 호출방식
     * 먼저 카메라포지션상속해제 + 메인플레이어 STEP.NOACTION 
     이동(카메라포지션)+회전(메인카메라) t_destination,r_destination초기값주고 MoveCamera() 호출
     회전(메인카메라) t_destination(this.tranform.position.x,this.tranform.position.y,this.tranform.position.z)
    */

    public void MoveCamera(Vector3 startRotation, Vector3 destinationRotation , float rotationTime, int _playerStep)//한번만호출
    {
        //고정좌표값 저장
        rCameraVector = destinationRotation;

        int plus_rotation_x_int;
        int minus_rotation_x_int;
        int destination_rotation_x_int;
        int plus_rotation_y_int;
        int minus_rotation_y_int;
        int destination_rotation_y_int;
        int plus_rotation_z_int;
        int minus_rotation_z_int;
        int destination_rotation_z_int;

        int plusValue_x = 0;
        int minusValue_x = 0;
        int plusValue_y = 0;
        int minusValue_y = 0;
        int plusValue_z = 0;
        int minusValue_z = 0;

        //카메라 이동중
        move_camera = true;
        //회전값초기화
        plus_rotation_x_int = (int)startRotation.x;
        minus_rotation_x_int = (int)startRotation.x;
        destination_rotation_x_int = (int)destinationRotation.x;
        plus_rotation_y_int = (int)startRotation.y;
        minus_rotation_y_int = (int)startRotation.y;
        destination_rotation_y_int = (int)destinationRotation.y;
        plus_rotation_z_int = (int)startRotation.z;
        minus_rotation_z_int = (int)startRotation.z;
        destination_rotation_z_int = (int)destinationRotation.z;

        //Debug.Log(plus_rotation_y_int.ToString() + " " + destination_rotation_y_int.ToString());

        //정수변환값 동일 -> start와 destination값 1미만 -> 회전X
        if (plus_rotation_x_int == destination_rotation_x_int)
            camera_rotation_x_arrive = true;
        if (plus_rotation_y_int == destination_rotation_y_int)
            camera_rotation_y_arrive = true;
        if (plus_rotation_y_int == destination_rotation_y_int)
            camera_rotation_y_arrive = true;

        //회전값계산++
        while (true)
        {
            if (plus_rotation_x_int == 360)
                plus_rotation_x_int = 0;
            if (plus_rotation_x_int == destination_rotation_x_int)
                break;
            plus_rotation_x_int++;
            plusValue_x++;
        }
        while (true)
        {
            plus_rotation_y_int++;
            plusValue_y++;
            if (plus_rotation_y_int == 360)
                plus_rotation_y_int = 0;
            if (plus_rotation_y_int == destination_rotation_y_int)
                break;
        }
        while (true)
        {
            if (plus_rotation_z_int == 360)
                plus_rotation_z_int = 0;
            if (plus_rotation_z_int == destination_rotation_z_int)
                break;
            plus_rotation_z_int++;
            plusValue_z++;
        }
        //회전값계산--
        while (true)
        {
            minus_rotation_x_int--;
            minusValue_x++;
            if (minus_rotation_x_int < 0)
                minus_rotation_x_int = 360;
            if (minus_rotation_x_int == destination_rotation_x_int)
                break;
        }
        while (true)
        {
            minus_rotation_y_int--;
            minusValue_y++;
            if (minus_rotation_y_int < 0)
                minus_rotation_y_int = 360;
            if (minus_rotation_y_int == destination_rotation_y_int)
                break;
        }
        while (true)
        {
            minus_rotation_z_int--;
            minusValue_z++;
            if (minus_rotation_z_int < 0)
                minus_rotation_z_int = 360;
            if (minus_rotation_z_int == destination_rotation_z_int)
                break;
        }
        //결과
        if (plusValue_x >= minusValue_x)
        {
            rotationAntiClockWise_x = true;//회전방향
            rotationValue_x = minusValue_x;//회전값
        }
        else
        {
            rotationClockWise_x = true;//회전방향
            rotationValue_x = plusValue_x;//회전값
        }

        if (plusValue_y >= minusValue_y)
        {
            rotationAntiClockWise_y = true;//회전방향
            rotationValue_y = minusValue_y;//회전값
        }
        else
        {
            rotationClockWise_y = true;//회전방향
            rotationValue_y = plusValue_y;//회전값
        }

        if (plusValue_z >= minusValue_z)
        {
            rotationAntiClockWise_z = true;//회전방향
            rotationValue_z = minusValue_z;//회전값
        }
        else
        {
            rotationClockWise_z = true;//회전방향
            rotationValue_z = plusValue_z;//회전값
        }

        //회전속도
        /*rotation_time = Vector3.Distance(this.transform.position, t_destination) * 0.1f; //거리비례 
        if (rotation_time < 2.0f)
            rotation_time = 2.0f;*/
        rotation_time = rotationTime;
        rotation_speed_x = rotationValue_x / rotation_time;
        rotation_speed_y = rotationValue_y / rotation_time;
        rotation_speed_z = rotationValue_z / rotation_time;

        //이동속도
        position_speed = Vector3.Distance(this.transform.position, t_destination) / rotation_time;

        //플레이어상태
        playerStep = _playerStep;

        //회전시작값 초기화
        camera_rotation_x = this.transform.localEulerAngles.x;
        camera_rotation_y = this.transform.localEulerAngles.y;
        camera_rotation_z = this.transform.localEulerAngles.z;

       // Debug.Log("x: " + rotationValue_x + " y: " + rotationValue_y  + " z: " + rotationValue_z + " 목적지:" + destinationRotation + " 시작지:" + startRotation + "");
       // Debug.Log("x: " + t_destination.x + " y: " + t_destination.y + " z: " + t_destination.z + " 포지션목적지:" + camera_position_destination);

    }//end of MoveCamera

    public void MoveOnlyCamera(Vector3 Position, Vector3 destinationPosition)//한번만호출
    {
        //고정좌표값 저장
        tCameraPVector = destinationPosition;
        //이동시작
        moveOnly_camera = true;
        //도착지점
        camera_position_destination = destinationPosition;
        //이동방향
        if (Position.x < destinationPosition.x)
        {
            camera_x_plus = true;
        }
        else
        {
            camera_x_minus = true;
        }

        if (Position.y < destinationPosition.y)
        {
            camera_y_plus = true;
        }
        else
        {
            camera_y_minus = true;
        }

        if (Position.z < destinationPosition.z)
        {
            camera_z_plus = true;
        }
        else
        {
            camera_z_minus = true;
        }

        //이동시작값 초기화
       /* camera_position_x = this.transform.localPosition.x;
        camera_position_y = this.transform.localPosition.y;
        camera_position_z = this.transform.localPosition.z;*/
    }//end of MoveOnlyCamera

    }
